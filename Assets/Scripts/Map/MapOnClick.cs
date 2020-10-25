using Interact;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Util;

namespace Map {
    public class MapOnClick : MonoBehaviour {
        [FormerlySerializedAs("colliderMaster")]
        public Transform blockMaster;
        public GameObject blockWrapperPrefab;

        public readonly float Side = 2.300f;
        public readonly float Step = 2.304f;
        public readonly Vector2Int Num = new Vector2Int(25, 25);
        public readonly Vector2 BottomLeft = new Vector2(-27.648f, -27.648f);

        private MainIA mainIA;
        private RaycastHit2D[] targetBlock;
        private int layerMask;

        [ContextMenu("Generate Map Colliders")]
        public void GenerateColliders() {
            var pos = BottomLeft;
            for(var y = 0; y < Num.y; ++y) {
                for(var x = 0; x < Num.x; ++x) {
                    var go = Instantiate(blockWrapperPrefab, blockMaster, false);
                    go.transform.position = pos;
                    go.name = $"MapBlockCollider {x} {y}";
                    pos.x += Step;
                }
                pos.x = BottomLeft.x;
                pos.y += Step;
            }
        }

        private Vector2 mousePos;

        private void OnMove(InputAction.CallbackContext ctx) {
            mousePos = ctx.ReadValue<Vector2>();
        }


        private void OnConfirm(InputAction.CallbackContext ctx) {
            var ray = SceneObjRef.Instance.MainCamera.ScreenPointToRay(mousePos);
            var cnt = Physics2D.GetRayIntersectionNonAlloc(ray, targetBlock, 10, layerMask);
            if(cnt < 1) {
                Debug.Log($"F{Time.frameCount} NO HIT");
                return;
            }
            Debug.Log($"F{Time.frameCount} {targetBlock[0].transform.name}");
        }


        private void Awake() {
            targetBlock = new RaycastHit2D[1];
            layerMask = 1 << LayerMask.NameToLayer("MapBlock");

            mainIA = new MainIA();
            mainIA.MapControl.MouseMove.performed += OnMove;
            mainIA.MapControl.PlaceBlock.performed += OnConfirm;
        }

        private void OnEnable() {
            mainIA.Enable();
        }

        private void OnDisable() {
            mainIA.Disable();
        }
    }
}
