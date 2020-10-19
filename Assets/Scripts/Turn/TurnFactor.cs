using UnityEngine;

namespace Turn {
    [CreateAssetMenu(fileName = "TurnFactorList", menuName = "Turn Based Fx/Turn Factor List")]
    public class TurnFactorList : ScriptableObject {
        public float[] factors;
    }
}
