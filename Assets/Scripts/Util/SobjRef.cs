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
            MapManager.CreateInstance(Resources.Load<MapDescSobj>("Map/Map0"));

            // Scriptable Objects
            PropFactorList = Resources.Load<TurnFactorList>("Event/PropertyFactor");
            CatasFactorList = Resources.Load<TurnFactorList>("Event/CatastropheFactor");
            LoadBuildingSobjs();
            LoadEventSobjs();
            LoadTreeItemSobjs();

            InitBuildingSobjs();
            InitEventSobjs();
            InitTreeItemSobjs();

            // Building Levels
            BuildingLevels.CreateInstance(BuildingDict);
        }


        public TurnFactorList PropFactorList { get; private set; }
        public TurnFactorList CatasFactorList { get; private set; }
        public NameidSobjDict<BuildingDescription> BuildingDict { get; private set; }
        public NameidSobjDict<EventSobj> EventDict { get; private set; }
        public NameidSobjDict<TreeItemSobj> TreeItemDict { get; private set; }


        private void LoadBuildingSobjs() {
            var sobjs = Resources.LoadAll<BuildingDescription>("Building");
            BuildingDict = new NameidSobjDict<BuildingDescription>(sobjs);
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
            }
        }

        private void InitEventSobjs() {
            // 采集部队 > 遇难 > 数回合内禁用该事件
            EventDict["cai_ji_bu_dui"].options[1].results[2].wrapper.OnTrigger = result => {
                EventResultWrapper.DefaultOnTrigger(result);
                BuffQueue.Instance.Add(new EventMaskBuff(result.EventSource.wrapper, 3));
            };
            EventDict["cai_ji_bu_dui"].options[2].results[2].wrapper.OnTrigger = result => {
                EventResultWrapper.DefaultOnTrigger(result);
                BuffQueue.Instance.Add(new EventMaskBuff(result.EventSource.wrapper, 3));
            };
            EventDict["cai_ji_bu_dui"].options[3].results[2].wrapper.OnTrigger = result => {
                EventResultWrapper.DefaultOnTrigger(result);
                BuffQueue.Instance.Add(new EventMaskBuff(result.EventSource.wrapper, 3));
            };


            // 动员 > 动荡的人心 > 数回合内禁用该事件
            EventDict["dong_yuan"].options[1].results[2].wrapper.OnTrigger = result => {
                EventResultWrapper.DefaultOnTrigger(result);
                BuffQueue.Instance.Add(new EventMaskBuff(result.EventSource.wrapper, 3));
            };

            EventDict["dong_yuan"].options[2].results[2].wrapper.OnTrigger = result => {
                EventResultWrapper.DefaultOnTrigger(result);
                BuffQueue.Instance.Add(new EventMaskBuff(result.EventSource.wrapper, 3));
            };

            EventDict["dong_yuan"].options[3].results[2].wrapper.OnTrigger = result => {
                EventResultWrapper.DefaultOnTrigger(result);
                BuffQueue.Instance.Add(new EventMaskBuff(result.EventSource.wrapper, 3));
            };


            // 日食 > 免疫
            EventDict["ri_shi"].options[0].results[0].wrapper.CanTrigger =
                delegate { return !TreeItemDict["chu_deng_jiao_yu_fa"].wrapper.Unlocked; };
            EventDict["ri_shi"].options[0].results[1].wrapper.CanTrigger =
                delegate { return TreeItemDict["chu_deng_jiao_yu_fa"].wrapper.Unlocked; };


            // 洪水 > 破坏建筑
            EventDict["hong_shui"].options[0].wrapper.OnSelected = option => {
                var flag = false;
                // Map.has("shui_ba")
                // todo: 洪水 > 检查水坝
                Debug.LogWarning("[TODO] 洪水 > 检查水坝");
                if(flag)
                    return;
                // Map.dealDamage(type)
                // todo: 洪水 > 破坏建筑
                Debug.LogWarning("[TODO] 洪水 > 破坏建筑");
            };
            // rst0: 水坝; rst1: 电力水泵 rst2: 抽水泵; rst3: 破坏; 
            EventDict["hong_shui"].options[0].results[0].wrapper.CanTrigger = delegate {
                var flag = false;
                // Map.has("shui_ba")
                // todo: 洪水 > 建造水坝后免疫
                Debug.LogWarning("[TODO] 洪水 > 建造水坝后免疫");
                return flag;
            };
            EventDict["hong_shui"].options[0].results[0].wrapper.OnTrigger = delegate { };
            EventDict["hong_shui"].options[0].results[1].wrapper.CanTrigger =
                delegate { return TreeItemDict["dian_li_shui_beng"].wrapper.Unlocked; };
            EventDict["hong_shui"].options[0].results[1].wrapper.OnTrigger =
                result => EventResultWrapper.PropOnTrigger(result, .4f);
            EventDict["hong_shui"].options[0].results[2].wrapper.CanTrigger =
                delegate {
                    return /*!TreeItemDict["dian_li_shui_beng"].wrapper.Unlocked &&*/
                        TreeItemDict["shui_beng"].wrapper.Unlocked;
                };
            EventDict["hong_shui"].options[0].results[2].wrapper.OnTrigger =
                result => EventResultWrapper.PropOnTrigger(result, .2f);


            // 海啸 > 破坏建筑
            EventDict["hai_xiao"].options[0].wrapper.OnSelected = option => {
                var flag = false;
                // Map.has("gao_ji_shui_ba")
                // todo: 海啸 > 检查高级水坝
                Debug.LogWarning("[TODO] 海啸 > 检查高级水坝");
                if(flag)
                    return;
                // Map.dealDamage(type)
                // todo: 海啸 > 破坏建筑
                Debug.LogWarning("[TODO] 海啸 > 破坏建筑");
            };
            // rst0: 高级水坝; rst1: 电力水泵 rst2: 抽水泵; rst3: 破坏; 
            EventDict["hai_xiao"].options[0].results[0].wrapper.CanTrigger = delegate {
                var flag = false;
                // Map.has("gao_ji_shui_ba")
                // todo: 海啸 > 建造高级水坝后免疫
                Debug.LogWarning("[TODO] 海啸 > 建造高级水坝后免疫");
                return flag;
            };
            EventDict["hai_xiao"].options[0].results[0].wrapper.OnTrigger = delegate { };
            EventDict["hai_xiao"].options[0].results[1].wrapper.CanTrigger =
                delegate { return TreeItemDict["dian_li_shui_beng"].wrapper.Unlocked; };
            EventDict["hai_xiao"].options[0].results[1].wrapper.OnTrigger =
                result => EventResultWrapper.PropOnTrigger(result, .4f);
            EventDict["hai_xiao"].options[0].results[2].wrapper.CanTrigger =
                delegate {
                    return /*!TreeItemDict["dian_li_shui_beng"].wrapper.Unlocked &&*/
                        TreeItemDict["shui_beng"].wrapper.Unlocked;
                };
            EventDict["hai_xiao"].options[0].results[2].wrapper.OnTrigger =
                result => EventResultWrapper.PropOnTrigger(result, .2f);


            // 暴雨 > 破坏建筑
            EventDict["bao_yu"].options[0].wrapper.OnSelected = option => {
                // Map.dealDamage(type)
                // todo: 暴雨 > 破坏建筑
                Debug.LogWarning("[TODO] 暴雨 > 破坏建筑");
            };
            // rst0: 电力水泵 rst1: 抽水泵; rst2: 破坏; 
            EventDict["bao_yu"].options[0].results[0].wrapper.CanTrigger =
                delegate { return TreeItemDict["dian_li_shui_beng"].wrapper.Unlocked; };
            EventDict["bao_yu"].options[0].results[0].wrapper.OnTrigger =
                result => EventResultWrapper.PropOnTrigger(result, .4f);
            EventDict["bao_yu"].options[0].results[1].wrapper.CanTrigger =
                delegate {
                    return /*!TreeItemDict["dian_li_shui_beng"].wrapper.Unlocked &&*/
                        TreeItemDict["shui_beng"].wrapper.Unlocked;
                };
            EventDict["bao_yu"].options[0].results[1].wrapper.OnTrigger =
                result => EventResultWrapper.PropOnTrigger(result, .2f);


            // 地震 > 破坏建筑
            EventDict["di_zhen"].options[0].wrapper.OnSelected = option => {
                // Map.dealDamage(type)
                // todo: 地震 > 破坏建筑
                Debug.LogWarning("[TODO] 地震 > 破坏建筑");
            };


            // 飓风 > 破坏建筑
            EventDict["ju_feng"].options[0].wrapper.OnSelected = option => {
                // Map.dealDamage(type)
                // todo: 飓风 > 破坏建筑
                Debug.LogWarning("[TODO] 飓风 > 破坏建筑");
            };


            // 严寒 > 住宅保温免疫
            EventDict["yan_han"].options[0].results[0].wrapper.CanTrigger = delegate {
                return !TreeItemDict["zhu_zhai_bao_wen"].wrapper.Unlocked;
            };
            EventDict["yan_han"].options[0].results[1].wrapper.CanTrigger = delegate {
                return TreeItemDict["zhu_zhai_bao_wen"].wrapper.Unlocked;
            };


            // 酷暑 > 散热涂料免疫
            EventDict["ku_shu"].options[0].results[0].wrapper.CanTrigger = delegate {
                return !TreeItemDict["san_re_tu_liao"].wrapper.Unlocked;
            };
            EventDict["ku_shu"].options[0].results[1].wrapper.CanTrigger = delegate {
                return TreeItemDict["san_re_tu_liao"].wrapper.Unlocked;
            };


            // 叛乱 > 处死游行者
            EventDict["pan_luan"].options[3].wrapper.CanUnlock = option => {
                var flag0 = TreeItemDict["si_xing_fa_an"].wrapper.Unlocked;
                var factor = Extension.GetTurnFactor(EventType.Catastrophe);
                var flag1 = PropertyManager.Instance.CanSubtractProperty(option.chooseCost * factor);
                return flag0 && flag1;
            };


            // 宗教狂热行为


            // 实验失败
            // rst0: 实验理性; rst1: 混乱的实验室
            EventDict["shi_yan_shi_bai"].options[0].results[0].wrapper.CanTrigger = delegate {
                return TreeItemDict["shi_yan_li_xing"].wrapper.Unlocked ||
                       TreeItemDict["gao_deng_jiao_yu_fa"].wrapper.Unlocked;
            };
            EventDict["shi_yan_shi_bai"].options[0].results[0].wrapper.OnTrigger = result => {
                var i0 = TreeItemDict["shi_yan_li_xing"].wrapper.Unlocked ? 1 : 0;
                var i1 = TreeItemDict["gao_deng_jiao_yu_fa"].wrapper.Unlocked ? 1 : 0;
                // todo: 实验失败 > 禁用实验室
                Debug.LogWarning($"[TODO] 实验失败 > 禁用实验室 {4 - i0 - i1}");
            };
            EventDict["shi_yan_shi_bai"].options[0].results[1].wrapper.OnTrigger = result => {
                // todo: 实验失败 > 禁用实验室
                Debug.LogWarning("[TODO] 实验失败 > 禁用实验室 4");
            };


            // 辐射
            EventDict["fu_she"].options[0].results[0].wrapper.OnTrigger = result => {
                // todo: 辐射 > 【-Δ0.5资产】*资产生产建筑数量 持续回合：3
                Debug.LogWarning("[TODO] 辐射 > 【-Δ0.5资产】*资产生产建筑数量 持续回合：3");
            };


            // todo: other events
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
