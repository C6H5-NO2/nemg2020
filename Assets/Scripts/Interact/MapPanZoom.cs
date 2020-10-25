using UnityEngine;
using UnityEngine.InputSystem;
using Util;

namespace Interact {
    public class MapPanZoom : MonoBehaviour {
        public Camera mainCamera;
        [Header("Pan")] public float moveSpeed = 13;
        public Vector2 moveNegOffset = new Vector2(.1f, .1f);
        public Vector2 movePosOffset = new Vector2(.9f, .9f);
        /// <summary> The max pos camera can see. NOT the max camera pos. </summary>
        public Vector2 moveNegLimit = new Vector2(-28.8f, -28.8f);
        public Vector2 movePosLimit = new Vector2(28.8f, 28.8f);
        [Header("Zoom")] public float zoomSpeed = 2.3f;
        public Vector2 zoomLimit = new Vector2(4, 15.6f);

        public bool AllowClick { get; set; }


        private Vector2 mousePos;

        private void OnPan() {
            mousePos = Input.mousePosition;
            var cameraPos = mainCamera.transform.position;
            // move position
            var vPos = mainCamera.ScreenToViewportPoint(mousePos);
            var ratio = moveSpeed * Time.deltaTime;
            if(vPos.x < moveNegOffset.x)
                cameraPos += (Vector3.left * ratio);
            else if(vPos.x > movePosOffset.x)
                cameraPos += (Vector3.right * ratio);
            if(vPos.y < moveNegOffset.y)
                cameraPos += (Vector3.down * ratio);
            else if(vPos.y > movePosOffset.y)
                cameraPos += (Vector3.up * ratio);
            // limit position
            var halfHeight = mainCamera.orthographicSize;
            var halfWidth = mainCamera.aspect * halfHeight;
            cameraPos.x = Mathf.Clamp(cameraPos.x, moveNegLimit.x + halfWidth, movePosLimit.x - halfWidth);
            cameraPos.y = Mathf.Clamp(cameraPos.y, moveNegLimit.y + halfHeight, movePosLimit.y - halfHeight);
            mainCamera.transform.position = cameraPos;
        }

        private void OnZoom(InputAction.CallbackContext ctx) {
            var delta = -ctx.ReadValue<Vector2>().y;
            delta *= zoomSpeed * Time.deltaTime;
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize + delta,
                                                      zoomLimit.x,
                                                      zoomLimit.y);
        }


        private MainIA mainIA;

        private void Awake() {
            mainIA = new MainIA();
            //mainIA.MapControl.MouseMove.performed += OnPan;
            mainIA.MapControl.ZoomCamera.performed += OnZoom;
        }

        private void OnEnable() {
            mainIA.Enable();
        }

        private void OnDisable() {
            mainIA.Disable();
        }

        private void Update() {
            OnPan();
        }
    }
}
