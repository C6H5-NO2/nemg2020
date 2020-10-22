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

        public Func<EventResultWrapper, bool> CanTrigger = (wrapper) => true;

        public Action<EventResultWrapper> OnTrigger = DefaultOnTrigger;

        public static void DefaultOnTrigger(EventResultWrapper rst) {
            var factor = Extension.GetTurnFactor(rst.EventSource.type);
            var ins = rst.Sobj.instantOutcome * factor;
            if(ins.Any(p => p != 0))
                PropertyManager.Instance.AddProperty(ins);
            var buff = rst.Sobj.buffOutcome;
            if(buff.ActiveTurns > 0 && buff.PropertyGroup.Any(p => p != 0))
                BuffQueue.Instance.Add(buff.CreateBuff(factor));
        }
    }
}
