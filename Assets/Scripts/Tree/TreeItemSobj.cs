using Property;
using UnityEngine;
using Util;

namespace Tree {
    [CreateAssetMenu(fileName = "TreeItemSobj", menuName = "Tree/Item")]
    public class TreeItemSobj : IdSobj {
        [TextArea] public string subDescription;
        public PropertyReprGroup unlockCost;
        public TreeItemSobj[] prevTreeItems;

        // dynamic data
        public TreeItemWrapper wrapper;
    }
}
