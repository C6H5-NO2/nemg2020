using Loop;
using UnityEngine;
using UnityEngine.UI;

namespace Buggy {
    public class QuitGame : MonoBehaviour {
        public Button newGameBtn, infoBtn, quitBtn;

        private void Awake() {
            newGameBtn.onClick.AddListener(delegate {
                gameObject.SetActive(false);
                GameLoop.Instance.StartNewGame();
                // todo: set ui to continue
            });
            infoBtn.onClick.AddListener(delegate { Debug.Log("Gu Cube"); });
            quitBtn.onClick.AddListener(Application.Quit);
        }
    }
}
