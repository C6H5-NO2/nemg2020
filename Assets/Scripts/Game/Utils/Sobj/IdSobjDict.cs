using System;
using System.Collections;
using System.Collections.Generic;

namespace Game.Utils.Sobj {
    public class IdSobjDict<T> : IEnumerable<T> where T : IdSobj {
        private readonly Dictionary<int, T> idDict;
        private readonly Dictionary<string, int> nameidDict;

        public IdSobjDict(int capacity) {
            idDict = new Dictionary<int, T>(capacity);
            nameidDict = new Dictionary<string, int>(capacity);
        }


        public IdSobjDict(IReadOnlyCollection<T> collection) {
            var capacity = collection.Count;
            idDict = new Dictionary<int, T>(capacity);
            nameidDict = new Dictionary<string, int>(capacity);
            foreach(var sobj in collection)
                Add(sobj);
        }


        public void Add(T sobj) {
        #if UNITY_EDITOR
            DuplicationCheck(sobj);
        #endif
            idDict.Add(sobj.id, sobj);
            nameidDict.Add(sobj.nameid, sobj.id);
        }


        public IEnumerator<T> GetEnumerator() => idDict.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        public bool TryGetValue(int id, out T value) => idDict.TryGetValue(id, out value);

        public bool TryGetValue(string nameid, out T value) {
            var valid = nameidDict.TryGetValue(nameid, out var id);
            if(valid)
                return TryGetValue(id, out value);
            value = null;
            return false;
        }


        public T this[int id] => idDict[id];
        public T this[string nameid] => idDict[nameidDict[nameid]];

        public int NameidToId(string nameid) => nameidDict[nameid];
        public string IdToNameid(int id) => idDict[id].nameid;


        public bool ContinuityCheck(out int idx) {
            idx = 0;
            for(var i = 1; i <= idDict.Count; ++i)
                if(!idDict.TryGetValue(i, out _)) {
                    idx = i;
                    return false;
                }
            return true;
        }


        private void DuplicationCheck(T sobj) {
            if(sobj.id == 0)
                throw new ArgumentException($"IdSobj of name {sobj.readableName} contains reserved id 0");
            if(idDict.ContainsKey(sobj.id))
                throw new ArgumentException($"IdSobj of name {sobj.readableName} contains duplicated id");
            if(nameidDict.ContainsKey(sobj.nameid))
                throw new ArgumentException($"IdSobj of name {sobj.readableName} contains duplicated nameid");
        }
    }
}
