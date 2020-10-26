using Util;

namespace Event {
    public class EventManager : ManualSingleton<EventManager> {
        public EventManager() {
            eventQueue = new EventQueue(64);
        }

        public bool Empty() => eventQueue.Empty;

        public EventWrapper GetFrontEvent() => eventQueue.Front;

        public EventWrapper PullEvent() {
            return eventQueue.PopFront();
        }

        public void PushEvent(EventWrapper wrapper) {
            eventQueue.PushBack(wrapper);
        }

        public void AddEventToFront(EventWrapper wrapper) {
            eventQueue.PushFront(wrapper);
        }

        public void GenerateEvents() {
            var pool = SobjRef.Instance.EventDict;
            foreach(var ev in pool) {
                var wrapper = ev.wrapper;
                if(wrapper.TryTrigger())
                    eventQueue.PushBack(wrapper);
            }
        }

        private readonly EventQueue eventQueue;

        public override void OnReset() {
            eventQueue.Clear();
        }
    }
}
