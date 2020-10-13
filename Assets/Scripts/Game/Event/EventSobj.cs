using Game.Utils.Sobj;
using UnityEngine;

namespace Game.Event {
    [CreateAssetMenu(fileName = "EventSobj", menuName = "Event/Event")]
    public class EventSobj : IdSobj {
        public EventType type;
        public EventOptionSobj[] options;
    }
}
