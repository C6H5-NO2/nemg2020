using System;
using Property;
using UnityEngine;
using Util;

namespace Event {
    public class EventWrapper : IIdSobjWrapper<EventSobj> {
        public EventWrapper(EventSobj sobj) {
            Sobj = sobj;
            Sobj.wrapper = this;
            Probability = Sobj.initProbability;
        }

        // set to 0 to disable
        public float Probability { get; set; }

        public EventSobj Sobj { get; }

        public Func<int, PropertyReprGroup> GetActuralOutcome;
    }
}
