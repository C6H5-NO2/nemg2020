using Loop;
using Property;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Event {
    public class EventUI : MonoBehaviour {
        public TextMeshProUGUI title, mainDesc;
        public Image mainImage;
        public Transform optionList;
        public GameObject optionUIPrefab;
        // generate this btn dynamically?
        public Button continueButton;

        private EventOptionUI[] options;


        private void Awake() {
            continueButton.gameObject.SetActive(false);
            continueButton.onClick.AddListener(NextEvent);
        }


        /////////////////////////////////////////////////
        /// Event UI display
        /////////////////////////////////////////////////
        private EventWrapper eventWrapper;
        private EventResultWrapper resultWrapper;

        private void SetCurrentEvent(EventWrapper wrapper) {
            continueButton.gameObject.SetActive(false);
            resultWrapper = null;

            eventWrapper = wrapper;
            var sobj = wrapper.Sobj;
            title.text = sobj.readableName;
            mainDesc.text = sobj.mainDescription;
            mainImage.sprite = sobj.mainImage;

            optionList.gameObject.SetActive(true);
            var numOpts = sobj.options.Length;
            if(numOpts == 0) {
                NextEvent();
            }
            else {
                options = new EventOptionUI[numOpts];
                for(var i = 0; i < numOpts; ++i)
                    CreateOption(sobj.options[i], i);
            }
        }

        private void CreateOption(EventOptionSobj option, int idx) {
            var obj = Instantiate(optionUIPrefab, optionList, false);
            var scrp = obj.GetComponent<EventOptionUI>();
            options[idx] = scrp;
            scrp.SetOption(this, option, idx);
        }

        /// <summary> Called by <c>EventOptionUI</c> </summary>
        public void OnOptionSelect(int idx) {
            var option = eventWrapper.Sobj.options[idx];
            option.wrapper.OnSelected?.Invoke(option);

            // cache this (?)
            optionList.gameObject.SetActive(false);
            foreach(var opt in options) {
                Destroy(opt.gameObject);
            }

            var result = option.results.TryTrigger();
            if(result == null) {
                //Debug.LogError($"Result not found for {eventWrapper.Sobj.readableName} {option.mainDescription}");
                NextEvent();
                return;
            }

            mainDesc.text = result.substituteDescription;
            resultWrapper = result.wrapper;

            continueButton.gameObject.SetActive(true);
        }


        /////////////////////////////////////////////////
        /// Event processing
        /////////////////////////////////////////////////

        /// <summary> Called by <c>GameLoop</c> </summary>
        public void StartProcess() {
            NextEvent();
        }

        private void NextEvent() {
            if(resultWrapper != null) {
                resultWrapper.OnTrigger(resultWrapper);
                resultWrapper = null;
            }

            var evmgr = EventManager.Instance;
            if(!evmgr.Empty()) {
                var ev = evmgr.PullEvent();
                SetCurrentEvent(ev);
                return;
            }
            // disable canvas
            gameObject.SetActive(false);
            // clean up TMP
            title.text = "";
            mainDesc.text = "";
            mainImage.sprite = null;
            GameLoop.Instance.TransToMap();
        }

        // called when failed
        public void ForceEndProcess() {
            // clear queue
            // clear by GameLoop
            //EventManager.Instance.Clear()

            // disable canvas
            gameObject.SetActive(false);
            // clean up TMP
            title.text = "";
            mainDesc.text = "";
            mainImage.sprite = null;
        }
    }
}
