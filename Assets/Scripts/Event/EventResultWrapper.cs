using System;
using Property;
using Turn.Buff;
using Util;

namespace Event {
    public class EventResultWrapper : ISobjWrapper<EventResultSobj> {
        public EventResultWrapper(EventResultSobj sobj, EventOptionSobj option) {
            Sobj = sobj;
            sobj.wrapper = this;
            Option = option;
            EventSource = option.wrapper.EventSource;
        }

        public EventResultSobj Sobj { get; }
        public EventOptionSobj Option { get; }
        public EventSobj EventSource { get; }

        /// <remarks>
        /// Default to true. Can be overriden. CAN NOT be null.<br/>
        /// Use param-less delegate since in param might be removed in future.<br/>
        /// </remarks>
        public Predicate<EventResultWrapper> CanTrigger = (result) => true;

        /// <remarks> Set by default. Can be overriden. CAN NOT be null. </remarks>
        public Action<EventResultWrapper> OnTrigger = DefaultOnTrigger;

        public static void PropOnTrigger(EventResultWrapper result, float immune = 0) {
            var factor = Extension.GetTurnFactor(result.EventSource.type);
            factor *= (1 - immune);
            var ins = result.Sobj.instantOutcome * factor;
            if(ins.Any(p => p != 0))
                PropertyManager.Instance.AddProperty(ins);
            var buff = result.Sobj.buffOutcome;
            if(buff.ActiveTurns > 0 && buff.PropertyGroup.Any(p => p != 0))
                BuffQueue.Instance.Add(buff.CreateBuff(factor));
        }

        public static void DefaultOnTrigger(EventResultWrapper result) {
            PropOnTrigger(result, 0);
        }
    }
}
