using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Event {
    public class EventOptionUI : MonoBehaviour {
        public Button button;
        public TextMeshProUGUI description;

        private EventUI parentUI;
        private int idx;

        /// <summary> Called by <c>EventUI</c> i.e. the parent UI </summary>
        public void SetOption(EventUI parentUI, EventOptionSobj option, int idx) {
            this.parentUI = parentUI;
            this.idx = idx;
            description.text = option.mainDescription;
            button.interactable = option.wrapper.CanUnlock();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClick);
        }

        private void OnClick() {
            parentUI.OnOptionSelect(idx);
        }
    }
}
