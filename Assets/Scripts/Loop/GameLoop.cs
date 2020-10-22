using System;
using System.Collections;
using Event;
using Property;
using Turn;
using Turn.Buff;
using UnityEngine;
using Util;

namespace Loop {
    public enum GameState { Map, Event }


    [DefaultExecutionOrder(-10)]
    public class GameLoop : SingletonMono<GameLoop> {
        public GameState State { get; private set; }

        //////////////////////////////////////////////
        /// Debug play
        //////////////////////////////////////////////
        private void Update() {
            if(Input.GetKeyDown(KeyCode.P)) {
                PropertyManager.Instance.AddProperty(new PropertyReprGroup(50, 0, 50, 0));
            }
            else if(Input.GetKeyDown(KeyCode.Escape)) {
                Application.Quit();
            }
        }


        //////////////////////////////////////////////
        /// Events
        //////////////////////////////////////////////

        /// <summary> called by <c>EndTurnButton</c> </summary>
        public void TransToEvent() {
        #if GAME_DEBUG_MODE
            if(State == GameState.Event)
                Debug.LogError($"State is already {GameState.Event}");
        #endif
            State = GameState.Event;
            //OnTransToEvent?.Invoke();
            // todo: Collect prop from map here
            Debug.LogWarning("Collect prop from map here");
            StartEventLoop();
        }

        private void StartEventLoop() {
            var eventUI = SceneObjRef.Instance.EventUI;
            eventUI.gameObject.SetActive(true);

            // bug: debug code
            EventManager.Instance.GenerateEvents();

            eventUI.StartProcess();
        }


        //////////////////////////////////////////////
        /// Map
        //////////////////////////////////////////////
        //public event Action OnTransToMap;
        public void TransToMap() {
        #if GAME_DEBUG_MODE
            if(State == GameState.Map)
                Debug.LogError($"State is already {GameState.Map}");
        #endif
            State = GameState.Map;
            //OnTransToMap?.Invoke();
            // bug: ???
            // activate main UI
            SceneObjRef.Instance.MainCanvas.gameObject.SetActive(true);
            TurnCounter.Instance.NewTurn();
        }


        //////////////////////////////////////////////
        /// Round counter
        //////////////////////////////////////////////
        private int round;

        protected override void OnInstanceAwake() {
            round = 0;
        }


        // !!! GAME ENTRY FUNCTION !!!
        private void Start() {
            StartNewRound();
        }

        private void StartNewRound() {
            ++round;
            Debug.Log($"New Round: {round}");

            PropertyManager.Instance.OnReset();
            BuffQueue.Instance.OnReset();
            TurnCounter.Instance.OnReset();
            //TurnCounter.Instance.OnNewTurn += BuffQueue.Instance.OnNewTurn;

            // todo: add start event & trans to event
            //TransToMap();
        }
    }
}
