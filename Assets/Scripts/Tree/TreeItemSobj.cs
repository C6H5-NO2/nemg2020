using Property;
using UnityEngine;
using Util;

namespace Tree {
    public enum TreeType { Culture, Tech }

    [CreateAssetMenu(fileName = "TreeItemSobj", menuName = "Tree/Item")]
    public class TreeItemSobj : IdSobj {
        public TreeType type;
        [TextArea] public string subDescription;
        public PropertyReprGroup unlockCost;
        public TreeItemSobj[] prevTreeItems;

        // dynamic data
        public TreeItemWrapper wrapper;
    }
}
