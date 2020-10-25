using Map;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;

namespace Interact {
    public class MapOnClick : MonoBehaviour {
        public Transform blockMaster;

        private MainIA mainIA;
        private RaycastHit2D[] targetBlock;
        private int layerMask;


        private Vector2Int hitPos;

        private void OnMove(InputAction.CallbackContext ctx) {
            var mousePos = ctx.ReadValue<Vector2>();
            var ray = SceneObjRef.Instance.MainCamera.ScreenPointToRay(mousePos);
            var cnt = Physics2D.GetRayIntersectionNonAlloc(ray, targetBlock, 10, layerMask);
            if(cnt < 1) {
                //Debug.Log($"F{Time.frameCount} NO HIT");
                hitPos = new Vector2Int(-1, -1);
                return;
            }
            //Debug.Log($"F{Time.frameCount} {targetBlock[0].transform.name}");
            var subs = targetBlock[0].transform.name.Split(' ');
            var fx = int.TryParse(subs[1], out var x);
            var fy = int.TryParse(subs[2], out var y);
            if(fx && fy)
                hitPos = new Vector2Int(x, y);
            else
                hitPos = new Vector2Int(-1, -1);
        }


        private void OnConfirm(InputAction.CallbackContext ctx) {
            var x = hitPos.x;
            var y = hitPos.y;
            if(x < 0 || y < 0) {
                Debug.Log("[Map Click] pos < 0");
                return;
            }
            var build = SobjRef.Instance.BuildingDict["zhu_fang_lv1"];
            var flag = MapManager.Instance.CanSetBlock(x, y, build);
            if(!flag) {
                Debug.Log($"[Map Click] cannot set {x}, {y}");
                return;
            }

            MapManager.Instance.SetBlock(x, y, build);
            SceneObjRef.Instance.MapColliderUtil.GetUIBlock(x, y).SetSprite(build.mainImage, build.spriteOffset);
            Debug.Log($"[Map Click] set {x}, {y}");
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
