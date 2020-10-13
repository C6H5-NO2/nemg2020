using Game.Prop;
using UnityEngine;

namespace Game.Utils {
    [DefaultExecutionOrder(-10)]
    public class CachedObjRef : SingletonBase<CachedObjRef> {
        [SerializeField] private Camera mainCamera = null;
        [SerializeField] private SpriteRenderer followMouseBlockHolderRenderer = null;

        public Camera MainCamera => mainCamera;
        public SpriteRenderer FollowMouseBlockHolderRenderer => followMouseBlockHolderRenderer;


        public PropReprGroup GameProps { get; set; }

        protected override void OnInstanceAwake() {
            GameProps = new PropReprGroup();
        }
    }
}
