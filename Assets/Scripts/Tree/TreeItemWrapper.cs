using System;
using System.Linq;
using Util;

namespace Tree {
    public class TreeItemWrapper : IIdSobjWrapper<TreeItemSobj>, IUnlockable {
        public TreeItemWrapper(TreeItemSobj sobj) {
            Sobj = sobj;
            Sobj.wrapper = this;
            
        }

        public TreeItemSobj Sobj { get; }


        /// <returns> True for can unlock, false otherwise. </returns>
        public Func<TreeItemWrapper, bool> unlockPredicate;

        public Action<TreeItemWrapper> onUnlocked;

        public bool Unlocked { get; private set; }

        public bool CanUnlock() {
            if(Unlocked)
                return false;

            // trigger event instead?
            if(!SingleObjRef.Instance.PropertyManagerInstance.CanSubtractProperty(Sobj.unlockCost))
                return false;

            if(Sobj.prevTreeItems.Any(item => !item.wrapper.Unlocked))
                return false;

            if(!(unlockPredicate?.Invoke(this) ?? true))
                return false;

            return true;
        }

        public bool TryUnlock() {
            if(!CanUnlock())
                return false;
            SingleObjRef.Instance.PropertyManagerInstance.SubtractProperty(Sobj.unlockCost);
            Unlocked = true;
            onUnlocked?.Invoke(this);
            return true;
        }

        /// <summary> Call this when rebuilding the tree </summary>
        public void ForceUnlock() {
            Unlocked = true;
            onUnlocked?.Invoke(this);
        }

        public void Lock() {
            Unlocked = false;
        }
    }
}
