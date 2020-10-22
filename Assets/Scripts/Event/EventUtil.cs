using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Event {
    public static class EventUtil {
        public const float Tolerance = 1e-4f;

        public static bool TryTrigger(this EventWrapper wrapper) {
            if(wrapper.AlwaysTrigger())
                return true;
            if(wrapper.NeverTrigger())
                return false;
            var rand = Random.Range(.0f, 1);
            return rand < wrapper.Probability;
        }

        public static bool AlwaysTrigger(this EventWrapper wrapper)
            => 1 - wrapper.Probability < Tolerance;

        public static bool NeverTrigger(this EventWrapper wrapper)
            => wrapper.Probability < Tolerance;

        public static EventResultSobj TryTrigger(this EventResultSobj[] results) {
            var rand = Random.Range(.0f, 1);
            var prob = .0f;
            foreach(var result in results) {
                prob += result.probability;
                if(rand < prob) {
                    if(result.wrapper.CanTrigger(result.wrapper))
                        return result;
                }
            }
            return null;
        }
    }
}
