using Loop;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace UI {
    public class ReturnMenuButton : MonoBehaviour {
        private void Awake() {
            GetComponent<Button>().onClick.AddListener(delegate {
                // todo
            });
        }
    }
}
