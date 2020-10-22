using System;
using Property;
using UnityEngine;
using Util;

namespace Event {
    public class EventWrapper : ISobjWrapper<EventSobj> {
        public EventWrapper(EventSobj sobj) {
            Sobj = sobj;
            Sobj.wrapper = this;
            Probability = Sobj.initProbability;

            foreach(var option in sobj.options) {
                option.wrapper = new EventOptionWrapper(option, sobj);
            }
        }

        private float probability;
        // set to 0 to disable
        public float Probability {
            get => probability;
            set => probability = Mathf.Clamp(value, .0f, 1);
        }

        public EventSobj Sobj { get; }
    }
}
