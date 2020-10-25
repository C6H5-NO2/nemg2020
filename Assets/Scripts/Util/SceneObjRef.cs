using Event;
using Map;
using UI;
using UnityEngine;

namespace Util {
    [DefaultExecutionOrder(-20)]
    public class SceneObjRef : SingletonMono<SceneObjRef> {
        [SerializeField] private Camera mainCamera = null;
        public Camera MainCamera => mainCamera;

        [Header("Overlay Canvas")] [SerializeField]
        private GamePropUI gamePropUI = null;
        public GamePropUI GamePropUI => gamePropUI;

        [Header("Main Canvas")] [SerializeField]
        private Canvas mainCanvas = null;
        public Canvas MainCanvas => mainCanvas;

        [Header("Event Canvas")] [SerializeField]
        private EventUI eventUI = null;
        public EventUI EventUI => eventUI;

        [Header("Map")] [SerializeField] private MapColliderUtil mapColliderUtil = null;
        public MapColliderUtil MapColliderUtil => mapColliderUtil;


        protected override void OnInstanceAwake() { }
    }
}
