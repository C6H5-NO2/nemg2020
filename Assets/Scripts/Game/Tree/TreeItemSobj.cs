using System;
using System.Linq;
using Game.Prop;
using Game.Utils;
using Game.Utils.Sobj;
using UnityEngine;

namespace Game.Tree {
    [CreateAssetMenu(fileName = "TreeItemSobj", menuName = "Tree/Item")]
    public class TreeItemSobj : IdSobj, IUnlockable {
        public TreeItemSobj[] prevItems;
        public PropReprGroup unlockRes;

        public bool Locked { get; private set; }

        /// <summary> Set in dict </summary>
        public Action onUnlock;
        /// <summary> Set in dict </summary>
        /// <returns> True for can unlock, false otherwise. </returns>
        public Func<TreeItemSobj, bool> unlockValidate;


        public bool CanUnlock() {
            if(!Locked)
                return false;

            if(!prevItems.Any(item => item.Locked))
                return false;

            // trigger event instead
            //if(unlockRes.Any(res => !PropManager.Instance.CanSubtractProp(res)))
            //    return false;

            if(!(unlockValidate?.Invoke(this) ?? true))
                return false;

            return true;
        }


        public bool Unlock() {
            if(!CanUnlock())
                return false;
            Locked = false;
            CachedObjRef.Instance.GameProps -= unlockRes;
            onUnlock?.Invoke();
            return true;
        }


        /// <summary> Call this when rebuilding the tree </summary>
        public void ForceUnlock() {
            Locked = false;
            onUnlock?.Invoke();
        }
    }
}
