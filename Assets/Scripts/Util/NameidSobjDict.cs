using System;
using System.Collections;
using System.Collections.Generic;

namespace Util {
    public class NameidSobjDict<T> : IEnumerable<T> where T : IdSobj {
        private readonly Dictionary<int, string> idDict;
        private readonly Dictionary<string, T> nameidDict;

        public NameidSobjDict(int capacity) {
            idDict = new Dictionary<int, string>(capacity);
            nameidDict = new Dictionary<string, T>(capacity);
        }


        public NameidSobjDict(IReadOnlyCollection<T> collection) {
            var capacity = collection.Count;
            idDict = new Dictionary<int, string>(capacity);
            nameidDict = new Dictionary<string, T>(capacity);
            foreach(var sobj in collection)
                Add(sobj);
        }


        public void Add(T sobj) {
        #if GAME_DEBUG_MODE
            DuplicationCheck(sobj);
        #endif
            idDict.Add(sobj.id, sobj.nameid);
            nameidDict.Add(sobj.nameid, sobj);
        }


        public IEnumerator<T> GetEnumerator() => nameidDict.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        public bool TryGetValue(string nameid, out T value) => nameidDict.TryGetValue(nameid, out value);

        public bool TryGetValue(int id, out T value) {
            var valid = idDict.TryGetValue(id, out var nameid);
            if(valid)
                return TryGetValue(nameid, out value);
            value = null;
            return false;
        }


        public T this[int id] => nameidDict[idDict[id]];
        public T this[string nameid] => nameidDict[nameid];

        public int NameidToId(string nameid) => nameidDict[nameid].id;
        public string IdToNameid(int id) => idDict[id];


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
