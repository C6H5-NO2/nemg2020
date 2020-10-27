using System;
using Property;
using Turn;
using UnityEngine;
using EventType = Event.EventType;

namespace Util {
    public static class Extension {
        /// <summary>True if obj passes the lifetime check of the underlying engine object. False otherwise.</summary>
        /// buggy code: obj can be null
        //public static bool CheckLifetime(this UnityEngine.Object obj) => obj != null;
        public static float GetTurnFactor(EventType type) {
            var factor = 1.0f;
            var turnCount = TurnCounter.Instance.TurnCount;
            switch(type) {
                case EventType.Policy:
                    factor = SobjRef.Instance.PropFactorList.factors[turnCount];
                    break;
                case EventType.Catastrophe:
                    factor = SobjRef.Instance.CatasFactorList.factors[turnCount];
                    break;
            }
            return factor;
        }
    }
}
