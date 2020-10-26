using Map;
using Property;
using UnityEngine;
using Util;

namespace Building {
    public enum BuildingTag { Common, MainBase, Ruin, ResourcePoint }

    [CreateAssetMenu(fileName = "BuildingDescription", menuName = "Building/Description")]
    public class BuildingDescription : IdSobj, IUnlockable {
        public Vector2Int size;
        public PropertyReprGroup buildCost;
        public PropertyReprGroup product;
        public Sprite uiIcon;
        public Vector2 spriteOffset;
        public bool initAsLocked;
        public PlayerBuildingType playerBuildingType;
        public BuildingTag tag;
        public int level;
        public BuildingDescription nextLevel;

        // properties are NOT serialized by default
        public bool Unlocked { get; private set; }

        public bool CanUnlock() => true;

        public bool TryUnlock() {
            ForceUnlock();
            return true;
        }

        public void ForceUnlock() {
            Unlocked = true;
            if(playerBuildingType == PlayerBuildingType.Other)
                return;
            foreach(var block in MapManager.Instance.MapBlocksEnumerable) {
                if(block.State != BlockState.OccupiedBase)
                    continue;
                if(block.Building.playerBuildingType == playerBuildingType)
                    block.SetBuilding(this);
            }
        }

        public void Lock() {
            Unlocked = false;
        }
    }
}
