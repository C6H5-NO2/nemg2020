using System.Collections.Generic;
using Event;
using Map;
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
            if(Input.GetKeyDown(KeyCode.Equals)) {
                PropertyManager.Instance.AddProperty(new PropertyReprGroup(50, 0, 50, 0));
            }
            else if(Input.GetKeyDown(KeyCode.Minus)) {
                PropertyManager.Instance.SubtractProperty(new PropertyReprGroup(50, 0, 50, 0));
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

            // collect prop from map here
            PropertyManager.Instance.AddProperty(MapManager.Instance.CollectProducts());

            StartEventLoop();
        }

        private void StartEventLoop() {
            var eventUI = SceneObjRef.Instance.EventUI;
            eventUI.gameObject.SetActive(true);

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
            // activate main UI
            SceneObjRef.Instance.GameCanvas.gameObject.SetActive(true);
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
        public void StartNewGame() {
            StartNewRound();
        }

        public void StartNewRound() {
            var refs = SceneObjRef.Instance;
            refs.MainMenuCanvas.gameObject.SetActive(false);
            refs.GameCanvas.gameObject.SetActive(true);
            refs.OverlayCanvas.gameObject.SetActive(true);
            refs.EventCanvas.gameObject.SetActive(false);
            refs.TechTreeCanvas.gameObject.SetActive(false);
            refs.CultureTreeCanvas.gameObject.SetActive(false);

            ++round;

            PropertyManager.Instance.OnReset();
            BuffQueue.Instance.OnReset();
            TurnCounter.Instance.OnReset();
            //TurnCounter.Instance.OnNewTurn += BuffQueue.Instance.OnNewTurn;
            EventManager.Instance.OnReset();
            // other start-ups here
            SceneObjRef.Instance.MapColliderUtil.ClearColliders();
            SceneObjRef.Instance.MapColliderUtil.GenerateColliders();

            // add start event & trans to event
            var evDict = SobjRef.Instance.EventDict;
            EventManager.Instance.AddEventToFront(evDict["fu_su_de_huo_zhong"].wrapper);

            // special handling of turn 0
            State = GameState.Event;
            // setup main base
            var buildDict = SobjRef.Instance.BuildingDict;
            var mainBase = buildDict["zhu_ji_di_lv1"];
            var mapManager = MapManager.Instance;
            var mapSize = mapManager.MapDesc.mapSize;
            var poss = new List<Vector2Int>(mapSize.x * mapSize.y);
            for(var y = 0; y < mapSize.y; ++y) {
                for(var x = 0; x < mapSize.x; ++x) {
                    if(mapManager.CanSetBlock(x, y, mainBase)) {
                        poss.Add(new Vector2Int(x, y));
                    }
                }
            }
            var randIdx = Random.Range(0, poss.Count);
            var randPos = poss[randIdx];
            mapManager.SetBlock(randPos.x, randPos.y, mainBase);
            SceneObjRef.Instance.MapColliderUtil.GetUIBlock(randPos.x, randPos.y)
                       .SetSprite(mainBase.mainImage, mainBase.spriteOffset);

            var initProp = MapManager.Instance.CollectProducts();
            initProp += new PropertyReprGroup(100, 10, 100, 10);
            PropertyManager.Instance.AddProperty(initProp);

            var eventUI = SceneObjRef.Instance.EventUI;
            eventUI.gameObject.SetActive(true);
            eventUI.StartProcess();
        }
    }
}
