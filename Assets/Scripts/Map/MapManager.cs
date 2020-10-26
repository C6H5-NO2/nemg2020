using System;
using System.Collections.Generic;
using System.Linq;
using Building;
using Property;
using UnityEngine;
using Util;

namespace Map {
    public class MapManager : ManualSingleton<MapManager> {
        public MapManager(MapDescSobj desc) {
            MapDesc = desc;
        }

        private MapDescSobj mapDesc;
        public MapDescSobj MapDesc {
            get => mapDesc;
            private set {
                mapDesc = value;
                size = mapDesc.mapSize;
                map = new MapBlock[size.x, size.y];
                MapBlocksEnumerable = map.Cast<MapBlock>();
                OnReset();
            }
        }


        public IEnumerable<MapBlock> MapBlocksEnumerable { get; private set; }
        private MapBlock[,] map;
        private Vector2Int size;


        public MapBlock GetBlock(int x, int y) => map[x, y];

        public MapBlock GetBlockBase(int x, int y) {
            var block = map[x, y];
            if(block.State == BlockState.OccupiedPartial) {
                var basePos = block.BasePosition;
                return map[basePos.x, basePos.y];
            }
            return block;
        }


        public bool CanSetBlock(int x, int y, BuildingDescription build) {
            for(var yy = y; yy < y + build.size.y; ++yy)
                for(var xx = x; xx < x + build.size.x; ++xx) {
                    if(xx < 0 || xx >= size.x || yy < 0 || yy >= size.y)
                        return false;
                    if(map[xx, yy].State != BlockState.EmptyCanOccupy)
                        return false;
                }
            return true;
        }


        /// <summary> Call this on mouse click </summary>
        public void SetBlock(int x, int y, BuildingDescription build) {
            if(!CanSetBlock(x, y, build))
                return;
            map[x, y].SetBuilding(build);
        }


        //public bool DemolishBlock(int x, int y) {
        //    // todo
        //    return true;
        //}


        public PropertyReprGroup CollectProducts() {
            var product = new PropertyReprGroup();
            foreach(var block in map) {
                if(block.State != BlockState.OccupiedBase)
                    continue;
                var buildType = block.Building.playerBuildingType;
                if(buildType == PlayerBuildingType.Other)
                    continue;
                var treeDict = SobjRef.Instance.TreeItemDict;
                var baseProduct = block.Product;
                var scale = 1.0f;
                switch(buildType) {
                    case PlayerBuildingType.ZhuJiDi:
                        break;
                    case PlayerBuildingType.ZhuFang:
                        break;
                    case PlayerBuildingType.CaiJiZhan:
                        break;
                    case PlayerBuildingType.NongTian:
                        if(treeDict["feng_dong_li"].wrapper.Unlocked)
                            scale *= 1.2f;
                        if(treeDict["chuan_dong_ji_xie"].wrapper.Unlocked)
                            scale *= 1.2f;
                        if(treeDict["jing_mi_ji_xie"].wrapper.Unlocked)
                            scale *= 1.2f;
                        break;
                    case PlayerBuildingType.KuangJing:
                        if(treeDict["zhong_gu_ji_xie"].wrapper.Unlocked)
                            scale *= 2;
                        if(treeDict["zheng_qi_ji"].wrapper.Unlocked)
                            scale *= 2;
                        if(treeDict["qi_you_ji"].wrapper.Unlocked)
                            scale *= 1.5f;
                        break;
                    case PlayerBuildingType.KeYanJianZhu:
                        break;
                    case PlayerBuildingType.FaDianZhan:
                        break;
                }
                product += baseProduct * scale;
            }
            return product;
        }


        public sealed override void OnReset() {
            for(var y = 0; y < size.y; ++y)
                for(var x = 0; x < size.x; ++x)
                    map[x, y] = new MapBlock(x, y, mapDesc.blockDescs[x + y * size.x], this);
            // todo: clear ui colliders
        }
    }
}
