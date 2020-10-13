using Game.Utils;
using Game.Utils.Sobj;
using UnityEngine;

namespace Game.Event {
    [CreateAssetMenu(fileName = "EventOptionSobj", menuName = "Event/Option")]
    public class EventOptionSobj : IdSobj, IUnlockable {
        public EventSobj targetEvent;
        [TextArea] public string newDescription;

        public bool Locked { get; private set; }

        public bool CanUnlock() {
            return false;
        }

        public bool Unlock() {
            return false;
        }

        public void ForceUnlock() { }
    }
}
