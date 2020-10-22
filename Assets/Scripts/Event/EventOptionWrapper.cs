using System;
using Util;

namespace Event {
    public class EventOptionWrapper : ISobjWrapper<EventOptionSobj> {
        public EventOptionWrapper(EventOptionSobj sobj, EventSobj source) {
            Sobj = sobj;
            Sobj.wrapper = this;
            EventSource = source;
            foreach(var result in sobj.results) {
                var wrapper = new EventResultWrapper(result, sobj);
            }
        }

        public EventOptionSobj Sobj { get; }
        public EventSobj EventSource { get; }

        /// <summary> Fill when loaded </summary>
        public Func<bool> CanUnlock = () => true;

        /// <summary> Fill when loaded </summary>
        public Action<EventOptionSobj> OnSelected;
    }
}
