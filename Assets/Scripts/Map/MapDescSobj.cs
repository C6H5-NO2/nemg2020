using System;
using UnityEngine;

namespace Map {
    [CreateAssetMenu(fileName = "MapDescription", menuName = "Map/Description")]
    public class MapDescSobj : ScriptableObject {
        public Vector2Int mapSize;
        public BlockDesc[] blockDescs;
    }

    [Serializable]
    public struct BlockDesc {
        public bool canOccupy;
        public bool isCoast;
        public bool isRiverBank;
    }
}
