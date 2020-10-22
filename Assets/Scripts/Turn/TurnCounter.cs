using System;
using UnityEngine;
using Util;

namespace Turn {
    public class TurnCounter : ManualSingleton<TurnCounter> {
        public TurnCounter() {
            TurnCount = 0;
        }

        public int TurnCount { get; private set; }

        public event Action<int> OnNewTurn;

        public void NewTurn() {
            ++TurnCount;
            Debug.Log($"New turn: {TurnCount}");
            OnNewTurn?.Invoke(TurnCount);
        }

        public override void OnReset() {
            TurnCount = 1;
            // todo: set to 0 in release!!!
            //OnNewTurn = null;
        }
    }
}
