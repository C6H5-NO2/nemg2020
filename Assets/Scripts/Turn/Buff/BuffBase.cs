using System;

namespace Turn.Buff {
    public abstract class BuffBase : IBuff, IEquatable<BuffBase>, IComparable<BuffBase> {
        protected BuffBase(int activeTurns) {
            this.guid = Guid.NewGuid();
            this.activeTurns = activeTurns;
        }

        private readonly Guid guid;
        private int activeTurns;

        public int CompareTo(BuffBase other) {
            return activeTurns.CompareTo(activeTurns);
        }

        public bool Equals(BuffBase other) {
            return !(other is null) && guid.Equals(other.guid);
        }

        public override bool Equals(object obj) {
            if(obj is BuffBase buff)
                return Equals(buff);
            return false;
        }

        public override int GetHashCode() {
            return guid.GetHashCode();
        }

        public abstract void OnApplied();
        public abstract void OnRemoved();

        public virtual void OnNewTurn(int turn) => --activeTurns;
        public virtual bool IsActive() => activeTurns > 0;
    }
}
