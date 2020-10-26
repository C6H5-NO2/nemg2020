using Building;
using Map;
using Property;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;

namespace Interact {
    /// <summary> Follow Mouse on self </summary>
    public class MapOnPlaceBuild : MonoBehaviour {
        public Transform blockMaster;
        public SpriteRenderer followMouseHolder;

        public BuildingDescription TargetBuilding { get; private set; }


        //public bool Handling => enabled;

        // Check whether affordable BEFORE call
        public void StartHandle(BuildingDescription target) {
            enabled = true;
            TargetBuilding = target;
            hitPoint.Set(-1, -1);
            followMouseHolder.sprite = null;
            followMouseHolder.gameObject.SetActive(true);
        }


        private MainIA mainIA;
        private RaycastHit2D[] targetBlock;
        private int layerMask;
        private Camera mainCamera;


        //private bool hasHitPoint;
        //private Vector2 mousePos;
        private Vector2Int hitPoint;

        private void OnMove(InputAction.CallbackContext ctx) {
            hitPoint.Set(-1, -1);
            var mousePos = ctx.ReadValue<Vector2>();
            var ray = mainCamera.ScreenPointToRay(mousePos);
            var cnt = Physics2D.GetRayIntersectionNonAlloc(ray, targetBlock, 10, layerMask);

            // ---- Follow Mouse ----
            var worldPos = mainCamera.ScreenToWorldPoint(mousePos);
            worldPos.z = 0;
            followMouseHolder.transform.position = worldPos + (Vector3)TargetBuilding.spriteOffset;
            followMouseHolder.sprite = TargetBuilding.mainImage;
            var alpha = followMouseHolder.color.a;
            followMouseHolder.color = new Color(1, 0, 0, alpha);
            // --------

            if(cnt < 1) {
                //Debug.Log($"F{Time.frameCount} No hit");
                return;
            }

            //Debug.Log($"F{Time.frameCount} {targetBlock[0].transform.name}");
            var subs = targetBlock[0].transform.name.Split(' ');
            var fx = int.TryParse(subs[1], out var x);
            var fy = int.TryParse(subs[2], out var y);
            if(!fx || !fy)
                return;

            // check if building can be set on hit point
            var canSetBlock = MapManager.Instance.CanSetBlock(x, y, TargetBuilding);
            if(!canSetBlock)
                return;

            hitPoint.Set(x, y);
            followMouseHolder.color = new Color(0, 1, 0, alpha);
        }


        private void OnConfirm(InputAction.CallbackContext ctx) {
            var x = hitPoint.x;
            var y = hitPoint.y;
            if(x < 0 || y < 0) {
                //Debug.Log("[Map Click] pos < 0");
                return;
            }

            var build = TargetBuilding;
            var canSetBlock = MapManager.Instance.CanSetBlock(x, y, build);
            if(!canSetBlock) {
                //Debug.Log($"[Map Click] cannot set {x}, {y}");
                return;
            }

            PropertyManager.Instance.SubtractProperty(build.buildCost);
            MapManager.Instance.SetBlock(x, y, build);
            SceneObjRef.Instance.MapColliderUtil.GetUIBlock(x, y).SetSprite(build.mainImage, build.spriteOffset);
            //Debug.Log($"[Map Click] set {x}, {y}");

            followMouseHolder.gameObject.SetActive(false);
            followMouseHolder.sprite = null;
            enabled = false;
        }

        private void OnCancel(InputAction.CallbackContext ctx) {
            hitPoint.Set(-1, -1);

            followMouseHolder.gameObject.SetActive(false);
            followMouseHolder.sprite = null;
            enabled = false;
        }


        private void Awake() {
            targetBlock = new RaycastHit2D[1];
            layerMask = 1 << LayerMask.NameToLayer("MapBlock");
            mainCamera = SceneObjRef.Instance.MainCamera;
            hitPoint = new Vector2Int(-1, -1);

            mainIA = new MainIA();
            mainIA.MapControl.MouseMove.performed += OnMove;
            mainIA.MapControl.ConfirmPlaceBlock.performed += OnConfirm;
            mainIA.MapControl.CancelPlaceBlock.performed += OnCancel;

            enabled = false;
        }

        private void OnEnable() {
            mainIA.Enable();
        }

        private void OnDisable() {
            mainIA.Disable();
        }
    }
}
