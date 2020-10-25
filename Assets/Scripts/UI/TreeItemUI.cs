using Tree;
using UnityEngine;

namespace UI {
    public class TreeItemUI : MonoBehaviour {
        public TreeItemSobj sobj;
        private TreeItemWrapper wrapper;

        private void Start() {
            // todo: set ui
            wrapper = sobj.wrapper;
            var unlocked = wrapper.Unlocked;
            Debug.Log(wrapper);
            Debug.Log(unlocked);
            //var description = sobj.
        }
    }
}
