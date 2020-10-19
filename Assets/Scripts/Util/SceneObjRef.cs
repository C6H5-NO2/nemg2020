using UnityEngine;

namespace Util {
    [DefaultExecutionOrder(-10)]
    public class SceneObjRef : SingletonMono<SceneObjRef> {
        [SerializeField] private Camera mainCamera = null;
        public Camera MainCamera => mainCamera;

        protected override void OnInstanceAwake() { }
    }
}
