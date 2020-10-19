using UnityEngine;

namespace Util {
    public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T> {
        public static T Instance { get; private set; }

        protected abstract void OnInstanceAwake();

        private void Awake() {
            if(Instance.CheckLifetime())
                Destroy(this);
            else {
                Instance = (T)this;
                OnInstanceAwake();
            }
        }
    }
}
