using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class BuildBtnHolder : MonoBehaviour {
        public Button buildBtn;
        public BuildItemClickDesc buildItemClickDescUI;
        private bool isOn;
        private Animator animator;

        private void OnClick() {
            if(!isOn) {
                isOn = true;
                animator.Play("BuildBtnOn");
            }
            else {
                isOn = false;
                animator.Play("BuildBtnOff");
            }
            buildItemClickDescUI.gameObject.SetActive(false);
        }

        private void Awake() {
            isOn = false;
            animator = GetComponent<Animator>();
            buildBtn.onClick.AddListener(OnClick);
        }
    }
}
