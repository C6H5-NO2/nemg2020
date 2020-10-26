using Interact;
using Map;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Util {
    /// <summary> Util for setup data, DO NOT for production </summary>
    public class ClickSetBlock : MonoBehaviour {
        public MapDescSobj mapdescsobj;

        private Vector2 mousePos;
        private MainIA mainIA;
        private RaycastHit2D[] targetBlock;
        private int layerMask;

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
            var nameof = targetBlock[0].transform.name;
            var substrs = nameof.Split(' ');
            var x = int.Parse(substrs[1]);
            var y = int.Parse(substrs[2]);
            Debug.Log($"F{Time.frameCount}. ({x}, {y}) = {x + y * 25}");
            var spr = targetBlock[0].transform.GetComponentInChildren<SpriteRenderer>();
            if(spr.color.r > .5f) {
                spr.color = new Color(0, 1, 0, 1);
                mapdescsobj.blockDescs[x + y * 25].canOccupy = true;
            }
            else {
                spr.color = new Color(1, 1, 1, 0.2f);
                mapdescsobj.blockDescs[x + y * 25].canOccupy = false;
            }
        }


        private void Awake() {
            targetBlock = new RaycastHit2D[1];
            layerMask = 1 << LayerMask.NameToLayer("MapBlock");

            mainIA = new MainIA();
            mainIA.MapControl.MouseMove.performed += OnMove;
            mainIA.MapControl.ConfirmPlaceBlock.performed += OnConfirm;
        }

        private void OnEnable() {
            mainIA.Enable();
        }

        private void OnDisable() {
            mainIA.Disable();
        }
    }
}
