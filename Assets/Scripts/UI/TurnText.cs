using System;
using Turn;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class TurnText : MonoBehaviour {
        private Text text;

        private void Awake() {
            text = GetComponent<Text>();
            TurnCounter.Instance.OnNewTurn += turn => text.text = $"第{turn}年";
        }
    }
}
