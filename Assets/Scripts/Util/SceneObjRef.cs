using Event;
using Map;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Util {
    [DefaultExecutionOrder(-20)]
    public class SceneObjRef : SingletonMono<SceneObjRef> {
        [SerializeField] private Camera mainCamera = null;
        public Camera MainCamera => mainCamera;

        [Header("Overlay Canvas")]
        //
        [SerializeField]
        private Canvas overlayCanvas = null;
        public Canvas OverlayCanvas => overlayCanvas;
        [SerializeField] private GamePropUI gamePropUI = null;
        public GamePropUI GamePropUI => gamePropUI;

        [Header("Game Canvas")]
        //
        [FormerlySerializedAs("mainCanvas")]
        [SerializeField]
        private Canvas gameCanvas = null;
        public Canvas GameCanvas => gameCanvas;

        [Header("Event Canvas")]
        //
        [SerializeField]
        private Canvas eventCanvas = null;
        public Canvas EventCanvas => eventCanvas;
        [SerializeField] private EventUI eventUI = null;
        public EventUI EventUI => eventUI;

        [Header("Tree Canvas")]
        //
        [SerializeField]
        private Canvas techTreeCanvas = null;
        public Canvas TechTreeCanvas => techTreeCanvas;
        [SerializeField] private Canvas cultureTreeCanvas = null;
        public Canvas CultureTreeCanvas => cultureTreeCanvas;


        [Header("Map")]
        //
        [SerializeField]
        private MapColliderUtil mapColliderUtil = null;
        public MapColliderUtil MapColliderUtil => mapColliderUtil;

        [Header("Main Menu")]
        //
        [SerializeField]
        private Canvas mainMenuCanvas = null;
        public Canvas MainMenuCanvas => mainMenuCanvas;


        protected override void OnInstanceAwake() { }
    }
}
