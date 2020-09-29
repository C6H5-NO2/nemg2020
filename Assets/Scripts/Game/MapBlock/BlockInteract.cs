using Game.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.MapBlock {
    /// <summary> add to BlockManager </summary>
    public class BlockInteract : MonoBehaviour {
        private MainIA mainIA;

        //[SerializeField] private InputActionAsset iaa;

        private Transform focusedBlock;

        private void OnConfirm(InputAction.CallbackContext ctx) {
            //Debug.Log("Block Cliked!" + Time.frameCount);
            if(!focusedBlock.CheckLifetime())
                return;
            var spr = focusedBlock.GetComponentInChildren<SpriteRenderer>();

            var color = CachedObjRef.Instance.FollowMouseBlockHolderRenderer.color;
            color.a = 1;
            spr.color = color;
            CachedObjRef.Instance.FollowMouseBlockHolderRenderer.color
                = new Color(Random.Range(.0f, 1), Random.Range(.0f, 1), Random.Range(.0f, 1), .5f);
        }


        private RaycastHit2D[] targetBlock;
        private int layerMask;

        private void OnMove(InputAction.CallbackContext ctx) {
            var mousePos = ctx.ReadValue<Vector2>();

            // ---- test code ----
            var cor = CachedObjRef.Instance;
            var wp = cor.MainCamera.ScreenToWorldPoint(mousePos);
            wp.z = 0;
            cor.FollowMouseBlockHolderRenderer.transform.parent.position = wp;
            // ----   ----   ----

            var ray = CachedObjRef.Instance.MainCamera.ScreenPointToRay(mousePos);
            var cnt = Physics2D.GetRayIntersectionNonAlloc(ray, targetBlock, 10, layerMask);
            if(cnt < 1) {
                focusedBlock = null;
                return;
            }
            focusedBlock = targetBlock[0].transform.parent;
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
