using Util;

namespace State {
    public enum GameState { Map, Event }

    public class StateManager {
        public GameState State { get; private set; }

        public void Reset() {
            State = GameState.Event;
        }

        public void TransToMap() {
            State = GameState.Map;
        }

        public void TransToEvent() {
            State = GameState.Event;
            SingleObjRef.Instance.TurnCounterInstance.NewTurn();
        }
    }
}
