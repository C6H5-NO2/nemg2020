using Loop;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace UI {
    public class EndTurnButton : MonoBehaviour {
        private void Awake() {
            GetComponent<Button>().onClick.AddListener(delegate {
                // disable parent canvas
                //SceneObjRef.Instance.MainCanvas.gameObject.SetActive(false);
                transform.parent.gameObject.SetActive(false);
                GameLoop.Instance.TransToEvent();
            });
        }
    }
}
