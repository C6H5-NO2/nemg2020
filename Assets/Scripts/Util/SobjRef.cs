using System;
using Building;
using Event;
using Map;
using Property;
using Tree;
using Turn;
using Turn.Buff;
using UnityEngine;
using EventType = Event.EventType;

namespace Util {
    public class SobjRef : ManualSingleton<SobjRef> {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init() {
            //if(!(Instance is null)) {
            //    //if(Instance.CheckLifetime()) {
            //    Debug.LogError($"Instance of {nameof(SobjRef)} already exists");
            //}
            //else {
            //    Instance = new SobjRef();
            //    Instance.OnInit();
            //}
            CreateInstance().OnReset();
        }


        public override void OnReset() {
            // Manual Singletons
            PropertyManager.CreateInstance();
            BuffQueue.CreateInstance();
            EventManager.CreateInstance();
            TurnCounter.CreateInstance();
            TurnCounter.Instance.OnNewTurn += BuffQueue.Instance.OnNewTurn;
            MapManager.CreateInstance(4, 5);

            // Scriptable Objects
            PropFactorList = Resources.Load<TurnFactorList>("Event/PropertyFactor");
            CatasFactorList = Resources.Load<TurnFactorList>("Event/CatastropheFactor");
            //LoadBuildingSobjs();
            LoadEventSobjs();
            LoadTreeItemSobjs();

            //InitBuildingSobjs();
            InitEventSobjs();
            InitTreeItemSobjs();
        }


        public TurnFactorList PropFactorList { get; private set; }
        public TurnFactorList CatasFactorList { get; private set; }
        public NameidSobjDict<BuildingSobj> BuildingDict { get; private set; }
        public NameidSobjDict<EventSobj> EventDict { get; private set; }
        public NameidSobjDict<TreeItemSobj> TreeItemDict { get; private set; }


        private void LoadBuildingSobjs() {
            var sobjs = Resources.LoadAll<BuildingSobj>("Building");
            BuildingDict = new NameidSobjDict<BuildingSobj>(sobjs);
            foreach(var sobj in sobjs) {
                if(sobj.initAsLocked)
                    sobj.Lock();
                else
                    sobj.ForceUnlock();
            }
        }

        private void InitBuildingSobjs() { }


        private void LoadEventSobjs() {
            var sobjs = Resources.LoadAll<EventSobj>("Event");
            EventDict = new NameidSobjDict<EventSobj>(sobjs);
            foreach(var sobj in sobjs) {
                var wrapper = new EventWrapper(sobj);
                if(sobj.type == EventType.Policy) {
                    foreach(var option in sobj.options) {
                        option.wrapper.CanUnlock = delegate {
                            var factor = Extension.GetTurnFactor(EventType.Policy);
                            return PropertyManager.Instance.CanSubtractProperty(option.chooseCost * factor);
                        };
                    }
                }
            }
        }

        private void InitEventSobjs() {
            EventDict["cai_ji_bu_dui"].options[1].results[2].wrapper.OnTrigger = wrapper => {
                EventResultWrapper.DefaultOnTrigger(wrapper);
                BuffQueue.Instance.Add(new EventMaskBuff(wrapper.EventSource.wrapper, 3));
            };
            EventDict["cai_ji_bu_dui"].options[2].results[2].wrapper.OnTrigger = wrapper => {
                EventResultWrapper.DefaultOnTrigger(wrapper);
                BuffQueue.Instance.Add(new EventMaskBuff(wrapper.EventSource.wrapper, 3));
            };
            EventDict["cai_ji_bu_dui"].options[3].results[2].wrapper.OnTrigger = wrapper => {
                EventResultWrapper.DefaultOnTrigger(wrapper);
                BuffQueue.Instance.Add(new EventMaskBuff(wrapper.EventSource.wrapper, 3));
            };

            EventDict["ri_shi"].options[0].results[0].wrapper.CanTrigger =
                wrapper => !TreeItemDict["chu_deng_jiao_yu_fa"].wrapper.Unlocked;
            EventDict["ri_shi"].options[0].results[1].wrapper.CanTrigger =
                wrapper => TreeItemDict["chu_deng_jiao_yu_fa"].wrapper.Unlocked;
        }


        private void LoadTreeItemSobjs() {
            var sobjs = Resources.LoadAll<TreeItemSobj>("TreeItem");
            TreeItemDict = new NameidSobjDict<TreeItemSobj>(sobjs);
            foreach(var sobj in sobjs) {
                var wrapper = new TreeItemWrapper(sobj);
                wrapper.Lock();
            }
        }

        private void InitTreeItemSobjs() {
            // todo: check in themselves instead since TreeItem has state
            // lv1
            //TreeItemDict["sun_mao_jie_gou"].wrapper.onUnlocked = _ => BuildingDict["mu_wu"].ForceUnlock();
            TreeItemDict["jian_zhu_xia_shui"].wrapper.onUnlocked =
                _ => throw new NotImplementedException("jian_zhu_xia_shui unlock callback");
            //TreeItemDict["geng_li"].wrapper.onUnlocked = _ => BuildingDict["nong_tian"].ForceUnlock();
            TreeItemDict["cha_yu_yan"].wrapper.onUnlocked = _ => {
                var delta = PropertyManager.Instance.GameProp[PropertyType.PopulationDelta] * .2f;
                var prop = new PropertyRepr(PropertyType.PopulationDelta, (int)delta);
                PropertyManager.Instance.AddProperty(prop);
            };
            //TreeItemDict["ji_chu_ji_xie"].wrapper.onUnlocked = _ => BuildingDict["kuang_jing"].ForceUnlock();
            TreeItemDict["mu_zhi_shui_ba"].wrapper.onUnlocked =
                _ => throw new NotImplementedException("mu_zhi_shui_ba unlock callback");
            TreeItemDict["chuan_tong_liao_fa"].wrapper.onUnlocked =
                _ => throw new NotImplementedException("chuan_tong_liao_fa unlock callback");
            // lv2
            //TreeItemDict["zhuan_hun_jie_gou"].wrapper.onUnlocked = _ => BuildingDict["zhuan_lou"].ForceUnlock();
            TreeItemDict["jian_zhu_bao_wen"].wrapper.onUnlocked =
                _ => throw new NotImplementedException("jian_zhu_bao_wen unlock callback");
            //TreeItemDict["cha_yang"].wrapper.onUnlocked = _ => BuildingDict["shui_tian"].ForceUnlock();
            // todo
            TreeItemDict["feng_dong_li"].wrapper.onUnlocked = null;
            TreeItemDict["zhong_gu_ji_xie"].wrapper.onUnlocked = null;
            TreeItemDict["qing_tong_zhi_gou_jian"].wrapper.onUnlocked = null;
            TreeItemDict["hun_ning_shui_ba"].wrapper.onUnlocked = null;
            TreeItemDict["chou_shui_beng"].wrapper.onUnlocked = null;
            TreeItemDict["yi_liao_ji_gou"].wrapper.onUnlocked = null;
            // lv3
        }
    }
}
