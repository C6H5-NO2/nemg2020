using Property;
using Turn;
using UnityEngine;

namespace Event {
    [CreateAssetMenu(fileName = "EventResult", menuName = "Event/Result", order = 3)]
    public class EventResultSobj : ScriptableObject {
        public string readableName;
        [TextArea] public string substituteDescription;
        public float probability;
        public PropertyReprGroup instantOutcome;
        public PropertyBuffFactory buffOutcome;

        //[Space] [TextArea] public string comment;
        public EventResultWrapper wrapper;
    }
}
