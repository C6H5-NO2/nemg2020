using UnityEngine;
using Util;

namespace Building {
    public enum BuildingTag { Empty }

    [CreateAssetMenu(fileName = "BuildingSobj", menuName = "Building/Sobj")]
    public class BuildingSobj : IdSobj, IUnlockable {
        public BuildingTag tag;
        public bool initAsLocked;

        // properties are NOT serialized by default
        public bool Unlocked { get; private set; }

        public bool CanUnlock() => true;

        public bool TryUnlock() {
            ForceUnlock();
            return true;
        }

        public void ForceUnlock() {
            Unlocked = true;
            // todo: add call back to map
        }

        public void Lock() {
            Unlocked = false;
        }
    }
}
