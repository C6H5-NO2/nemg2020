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

        /// <remarks> Set by <c>EventWrapper</c>. Can be overriden. CAN NOT be null </remarks>
        public Predicate<EventOptionSobj> CanUnlock = (option) => true;

        /// <remarks> Set manually. CAN be null </remarks>
        public Action<EventOptionSobj> OnSelected;
    }
}
