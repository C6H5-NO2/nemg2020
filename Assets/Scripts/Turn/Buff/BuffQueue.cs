using System.Collections.Generic;

namespace Turn.Buff {
    // todo: optimize
    public class BuffQueue {
        public BuffQueue() {
            buffs = new List<IBuff>(64);
        }

        public void Add(IBuff buff) {
            buff.OnApplied();
            buffs.Add(buff);
            //buffs.Sort();
        }

        public void Clear() {
            buffs.ForEach(buff => buff.OnRemoved());
            buffs.Clear();
        }

        public void OnNewTurn(int turn) {
            RemoveDisactive();
            buffs.ForEach(buff => buff.OnNewTurn(turn));
        }

        public void Remove(IBuff buff) {
            if(buffs.Remove(buff))
                buff.OnRemoved();
        }

        public void RemoveDisactive() {
            buffs.RemoveAll(buff => {
                var pred = !buff.IsActive();
                if(pred)
                    buff.OnRemoved();
                return pred;
            });
        }

        private readonly List<IBuff> buffs;
    }
}
