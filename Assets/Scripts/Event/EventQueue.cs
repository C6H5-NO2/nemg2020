using Nito;

namespace Event {
    /// <summary> Events per turn. ALL events are in SingleObjRef.EventDict </summary>
    public class EventQueue {
        public EventQueue(int capacity) {
            deque = new Deque<EventWrapper>(capacity);
        }

        public bool Empty => deque.Count == 0;

        public EventWrapper Back {
            get => deque[deque.Count - 1];
            set => deque[deque.Count - 1] = value;
        }

        public EventWrapper Front {
            get => deque[0];
            set => deque[0] = value;
        }

        public void Clear() {
            deque.Clear();
        }

        public void PushBack(EventWrapper wrapper) {
            deque.AddToBack(wrapper);
        }

        public void PushFront(EventWrapper wrapper) {
            deque.AddToFront(wrapper);
        }

        public EventWrapper PopBack() {
            return deque.RemoveFromBack();
        }

        public EventWrapper PopFront() {
            return deque.RemoveFromFront();
        }

        private readonly Deque<EventWrapper> deque;
    }
}
