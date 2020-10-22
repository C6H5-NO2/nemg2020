using UnityEngine;
using Util;

namespace Event {
    public enum EventType { Policy, Catastrophe, Story }

    [CreateAssetMenu(fileName = "EventSobj", menuName = "Event/Event", order = 1)]
    public class EventSobj : IdSobj {
        public EventType type;
        public float initProbability = 1;
        public EventOptionSobj[] options;


        public EventWrapper wrapper;
    }
}
