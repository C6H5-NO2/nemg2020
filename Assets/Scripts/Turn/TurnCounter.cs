using System;

namespace Turn {
    public class TurnCounter {
        public TurnCounter() {
            TurnCount = 0;
        }

        public int TurnCount { get; private set; }

        public event Action<int> OnNewTurn;

        public void Reset() => TurnCount = 0;

        public void NewTurn() {
            ++TurnCount;
            OnNewTurn?.Invoke(TurnCount);
        }
    }
}
