using UnityEngine;
using Util;

namespace Event {
    public enum EventType { Policy, Catastrophe }

    [CreateAssetMenu(fileName = "EventSobj", menuName = "Event/Event")]
    public class EventSobj : IdSobj {
        public EventType type;
        public float initProbability;
        public EventWrapper wrapper;
    }
}
