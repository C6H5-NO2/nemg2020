using Building;
using Property;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class BuildItemClickDesc : MonoBehaviour {
        public Text typeText, productText, costText, lockedText;
        private const float LeftOffset = 100;

        public void SetDisplay(PlayerBuildingType type, Vector2 basePosition) {
            var sobj = BuildingLevels.Instance.GetCurrLevel(type);
            typeText.text = $"{sobj.readableName} - {sobj.level} 级";
            productText.text = $"人口：{sobj.product[PropertyType.Population]}  资产：{sobj.product[PropertyType.Finance]}";
            costText.text = $"人口：{sobj.buildCost[PropertyType.Population]}  资产：{sobj.buildCost[PropertyType.Finance]}";
            lockedText.enabled = !sobj.Unlocked;
            Debug.Log($"BA: {basePosition}");
            transform.position = basePosition - new Vector2(LeftOffset, 0);
            gameObject.SetActive(true);
        }

        public void ClearDisplay() {
            gameObject.SetActive(false);
        }
    }
}
