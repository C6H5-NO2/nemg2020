using Event;
using Turn.Buff;

namespace Turn {
    public class EventMaskBuff : BuffBase {
        public EventMaskBuff(EventWrapper wrapper, int activeTurns) : base(activeTurns) {
            this.wrapper = wrapper;
            probability = wrapper.Probability;
        }

        private readonly EventWrapper wrapper;
        private readonly float probability;

        public override void OnApplied() {
            wrapper.Probability = 0;
        }

        public override void OnRemoved() {
            wrapper.Probability = probability;
        }
    }
}
