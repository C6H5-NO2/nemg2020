using UnityEngine;

namespace Turn {
    [CreateAssetMenu(fileName = "TurnFactorList", menuName = "Event/Turn Factor List", order = 0)]
    public class TurnFactorList : ScriptableObject {
        public float[] factors;
    }
}
