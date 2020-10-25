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
                var optWrapper = option.wrapper = new EventOptionWrapper(option, sobj);
                switch(sobj.type) {
                    case EventType.Policy:
                        optWrapper.CanUnlock = optSobj => {
                            var factor = Extension.GetTurnFactor(EventType.Policy);
                            return PropertyManager.Instance.CanSubtractProperty(optSobj.chooseCost * factor);
                        };
                        break;
                    case EventType.Catastrophe:
                        optWrapper.CanUnlock = optionSobj => true;
                        break;
                    case EventType.Story:
                        break;
                }
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
