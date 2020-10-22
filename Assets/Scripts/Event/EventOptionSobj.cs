using System;
using Property;
using UnityEngine;

namespace Event {
    [CreateAssetMenu(fileName = "EventOption", menuName = "Event/Option", order = 2)]
    public class EventOptionSobj : ScriptableObject {
        [TextArea] public string mainDescription;
        public PropertyReprGroup chooseCost;
        public EventResultSobj[] results;


        public EventOptionWrapper wrapper;
    }
}
