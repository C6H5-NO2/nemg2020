using Interact;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;

namespace Map {
    /// <summary> add to blockMaster </summary>
    public class MapColliderUtil : MonoBehaviour {
        public Transform blockMaster;
        public GameObject blockWrapperPrefab;

        public static readonly float Side = 2.300f;
        public static readonly float Step = 2.304f;
        public static readonly Vector2Int Num = new Vector2Int(25, 25);
        public static readonly Vector2 BottomLeft = new Vector2(-27.648f, -27.648f);


        public MapBlockUI GetUIBlock(int x, int y) => uiBlocks[x, y];


        //[ContextMenu("Generate Map Colliders")]
        public void GenerateColliders() {
            var pos = BottomLeft;
            for(var y = 0; y < Num.y; ++y) {
                for(var x = 0; x < Num.x; ++x) {
                    var go = Instantiate(blockWrapperPrefab, blockMaster, false);
                    go.transform.position = pos;
                    go.name = $"MapBlockCollider {x} {y}";
                    var uiBlock = go.GetComponent<MapBlockUI>();
                    // comment this line when exec in context menu
                    uiBlocks[x, y] = uiBlock;
                    uiBlock.Position = new Vector2Int(x, y);

                    // bug: debug code
                    go.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled =
                        MapManager.Instance.GetBlock(x, y).RelLoc.HasFlag(BlockRelLoc.Common);

                    pos.x += Step;
                }
                pos.x = BottomLeft.x;
                pos.y += Step;
            }
        }


        public void ClearColliders() {
            var children = new Transform[blockMaster.childCount];
            var idx = 0;
            foreach(Transform child in blockMaster) {
                children[idx] = child;
                ++idx;
            }
            foreach(var child in children) {
                Destroy(child.gameObject);
            }
        }


        private MapBlockUI[,] uiBlocks;


        private void Awake() {
            uiBlocks = new MapBlockUI[Num.x, Num.y];
        }
    }
}
