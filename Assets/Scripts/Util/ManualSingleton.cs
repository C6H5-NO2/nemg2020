using System;
using UnityEngine;

namespace Util {
    public abstract class ManualSingleton<T> where T : class {
        public static T Instance { get; protected set; }

        public static T CreateInstance(params object[] ctorParams) {
            //#if GAME_DEBUG_MODE
            //#endif
            if(Instance is null) {
                try {
                    var types = new Type[ctorParams.Length];
                    for(var i = 0; i < ctorParams.Length; ++i)
                        types[i] = ctorParams[i].GetType();
                    var ctor = typeof(T).GetConstructor(types);
                    if(ctor is null)
                        throw new ArgumentException($"Constructor not found");
                    Instance = ctor.Invoke(ctorParams) as T;
                }
                catch(Exception e) {
                    Debug.LogError($"Failed to create instance of {typeof(T)}. {e.Message}");
                    Instance = null;
                }
            }
            else
                Debug.Log($"Instance of {typeof(T)} already exists");
            return Instance;
        }

        public abstract void OnReset();
    }
}
