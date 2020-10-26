using Building;
using Interact;
using Property;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UI {
    public class BuildItemClick : MonoBehaviour,
                                  IPointerEnterHandler,
                                  IPointerExitHandler,
                                  IPointerClickHandler {
        public PlayerBuildingType playerBuildingType;
        [FormerlySerializedAs("mapOnClick")] public MapOnPlaceBuild mapOnPlaceBuild;
        public BuildItemClickDesc buildItemClickDescUI;

        public void OnPointerEnter(PointerEventData eventData) {
            var sobj = BuildingLevels.Instance.GetCurrLevel(playerBuildingType);
            if(sobj == null)
                return;
            buildItemClickDescUI.SetDisplay(playerBuildingType, transform.position);
        }

        public void OnPointerExit(PointerEventData eventData) {
            buildItemClickDescUI.ClearDisplay();
        }

        public void OnPointerClick(PointerEventData eventData) {
            if(eventData.button != PointerEventData.InputButton.Left)
                return;
            var currbld = BuildingLevels.Instance.GetCurrLevel(playerBuildingType);
            if(!currbld.Unlocked) {
                // todo: UI
                //Debug.Log($"{currbld.readableName} 未解锁");
                return;
            }
            if(!PropertyManager.Instance.CanSubtractProperty(currbld.buildCost)) {
                // todo: UI
                Debug.Log($"{currbld.readableName} 无法支付代价");
                return;
            }
            mapOnPlaceBuild.StartHandle(currbld);
        }
    }
}
