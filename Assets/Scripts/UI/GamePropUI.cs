using Property;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class GamePropUI : MonoBehaviour {
        public Text population, populationDelta, finance, financeDelta;

        public void UpdateText() {
            var group = PropertyManager.Instance.GameProp;
            population.text = $"人口：{group[PropertyType.Population]}";
            populationDelta.text = $"人口增量：{group[PropertyType.PopulationDelta]}";
            finance.text = $"资产：{group[PropertyType.Finance]}";
            financeDelta.text = $"资产增量：{group[PropertyType.FinanceDelta]}";
        }
    }
}
