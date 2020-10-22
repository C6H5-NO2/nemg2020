using Building;
using Property;
using UnityEngine;
using Util;

namespace Map {
    public class MapBlock : ISobjWrapper<BuildingSobj> {
        public MapBlock(int x, int y) {
            Position = new Vector2Int(x, y);
        }

        public MapBlock(Vector2Int position) : this(position.x, position.y) { }

        public BuildingSobj Sobj { get; private set; }

        public Vector2Int Position { get; }

        public PropertyReprGroup Product { get; set; }

        // todo
        public BuildingTag Tag => Sobj ? Sobj.tag : BuildingTag.Empty;
    }
}
