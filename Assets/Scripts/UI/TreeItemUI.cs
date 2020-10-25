using Tree;
using UnityEngine;

namespace UI {
    public class TreeItemUI : MonoBehaviour {
        public TreeItemSobj sobj;
        private TreeItemWrapper wrapper;

        private void Awake() {
            // todo: set ui
            wrapper = sobj.wrapper;
            var unlocked = wrapper.Unlocked;
            //var description = sobj.
        }
    }
}
