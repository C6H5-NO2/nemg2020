using UnityEngine;

namespace Game.Utils {
    public class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T> {
        public static T Instance { get; private set; }

        protected virtual void OnInstanceAwake() { }

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
