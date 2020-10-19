using System.Collections.Generic;
using System.Linq;
using Property;
using UnityEngine;

namespace Map {
    public class MapManager {
        public MapManager(Vector2Int size) {
            mapBlocks = new MapBlock[size.x, size.y];
            MapBlocksEnumerable = mapBlocks.Cast<MapBlock>();
            for(var x = 0; x < size.x; ++x)
                for(var y = 0; y < size.y; ++y)
                    mapBlocks[x, y] = new MapBlock(x, y);
        }

        public MapManager(int x, int y) : this(new Vector2Int(x, y)) { }

        public IEnumerable<MapBlock> MapBlocksEnumerable { get; }

        //public MapBlock GetBlock(Vector2Int position) => GetBlock(position.x, position.y);
        public MapBlock GetBlock(int x, int y)
            => mapBlocks[x, y];

        public void AddBlock(int x, int y)
            => mapBlocks[x, y] = new MapBlock(new Vector2Int(x, y));

        public PropertyReprGroup CollectProducts() {
            var product = new PropertyReprGroup();
            foreach(var block in mapBlocks) {
                if(block is null)
                    continue;
                product += block.Product;
            }
            return product;
        }

        private MapBlock[,] mapBlocks;
    }
}
