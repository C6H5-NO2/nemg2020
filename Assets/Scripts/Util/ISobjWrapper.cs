using UnityEngine;

namespace Util {
    public interface ISobjWrapper<out T> where T : ScriptableObject {
        T Sobj { get; }
    }
}
