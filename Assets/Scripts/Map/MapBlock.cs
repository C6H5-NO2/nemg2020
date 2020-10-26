using System;
using Building;
using Property;
using UnityEngine;

namespace Map {
    public enum BlockState { EmptyCanOccupy, EmptyCannotOccupy, OccupiedBase, OccupiedPartial }

    [Flags]
    public enum BlockRelLoc { None = 0, Common = 1, Coast = 2, RiverBank = 4 }


    public class MapBlock {
        public MapBlock(int x, int y, BlockDesc desc, MapManager map = null) {
            Position = new Vector2Int(x, y);
            this.map = map ?? MapManager.Instance;
            State = desc.canOccupy ? BlockState.EmptyCanOccupy : BlockState.EmptyCannotOccupy;
            RelLoc = BlockRelLoc.None;
            if(desc.canOccupy)
                RelLoc |= BlockRelLoc.Common;
            if(desc.isCoast)
                RelLoc |= BlockRelLoc.Coast;
            if(desc.isRiverBank)
                RelLoc |= BlockRelLoc.RiverBank;
        }

        public Vector2Int Position { get; }
        private readonly MapManager map;
        public BlockState State { get; private set; }
        public BlockRelLoc RelLoc { get; }


        /// <summary> Force set. Validation done by manager. </summary>
        /// <remarks> Should only be called by manager. </remarks>
        public void SetBuilding(BuildingDescription build) {
            for(var y = Position.y; y < Position.y + build.size.y; ++y) {
                for(var x = Position.x; x < Position.x + build.size.x; ++x) {
                    var partialBlock = map.GetBlock(x, y);
                    partialBlock.State = BlockState.OccupiedPartial;
                    partialBlock.BasePosition = Position;
                }
            }

            State = BlockState.OccupiedBase;
            Product = build.product;
            Building = build;
        }


        /// <summary> Force clear. Validation done by manager. </summary>
        /// <remarks> Should only be called by manager. </remarks>
        public void ClearBuilding() {
            switch(State) {
                case BlockState.OccupiedBase:
                    for(var y = Position.y; y < Position.y + Building.size.y; ++y) {
                        for(var x = Position.x; x < Position.x + Building.size.x; ++x) {
                            var partialBlock = map.GetBlock(x, y);
                            partialBlock.State = BlockState.EmptyCanOccupy;
                        }
                    }
                    break;
                case BlockState.OccupiedPartial:
                    map.GetBlock(BasePosition.x, BasePosition.y).ClearBuilding();
                    break;
            }
            State = BlockState.EmptyCanOccupy;
        }


        /// <summary> only valid when <c>State</c> is <c>OccupiedBase</c>. </summary>
        public PropertyReprGroup Product { get; private set; }

        //public void SetFactor(float factor) => Product *= factor;


        /// <summary> only valid when <c>State</c> is <c>OccupiedBase</c>. </summary>
        public BuildingDescription Building { get; private set; }


        /// <summary> only valid when <c>State</c> is <c>OccupiedPartial</c>. </summary>
        public Vector2Int BasePosition { get; private set; }
    }
}
