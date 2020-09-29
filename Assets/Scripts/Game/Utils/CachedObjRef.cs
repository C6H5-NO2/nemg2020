using UnityEngine;

namespace Game.Utils {
    [DefaultExecutionOrder(-10)]
    public class CachedObjRef : SingletonBase<CachedObjRef> {
        [SerializeField] private Camera mainCamera = null;
        [SerializeField] private SpriteRenderer followMouseBlockHolderRenderer = null;

        public Camera MainCamera => mainCamera;
        public SpriteRenderer FollowMouseBlockHolderRenderer => followMouseBlockHolderRenderer;
    }
}
