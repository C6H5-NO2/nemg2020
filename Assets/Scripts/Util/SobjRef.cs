﻿using System;
using Building;
using Event;
using Loop;
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
            EventDict["hong_shui"].options[0].results[0].wrapper.CanTrigger =
                delegate { return TreeItemDict["mu_zhi_shui_ba"]; };
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
            EventDict["hai_xiao"].options[0].results[0].wrapper.CanTrigger =
                delegate { return TreeItemDict["hun_ning_shui_ba"]; };
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
                        TreeItemDict["chou_shui_beng"].wrapper.Unlocked;
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
                return !TreeItemDict["ge_re_tu_liao"].wrapper.Unlocked;
            };
            EventDict["ku_shu"].options[0].results[1].wrapper.CanTrigger = delegate {
                return TreeItemDict["ge_re_tu_liao"].wrapper.Unlocked;
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
                var cnt = 0;
                foreach(var block in MapManager.Instance.MapBlocksEnumerable) {
                    if(block.State != BlockState.OccupiedBase)
                        continue;
                    if(block.Building.playerBuildingType != PlayerBuildingType.Other)
                        ++cnt;
                }
                var finance = -(int)(cnt * 0.5f * Extension.GetTurnFactor(EventType.Catastrophe));
                var dec = new PropertyBuff(new PropertyReprGroup(finance: finance), 3);
                BuffQueue.Instance.Add(dec);
            };

            // 传染性事件


            // 压抑气氛 > 音乐狂欢节免疫
            EventDict["ya_yi_qi_fen"].options[0].results[0].wrapper.CanTrigger = delegate {
                return !TreeItemDict["yin_yue_kuang_huan_jie"].wrapper.Unlocked;
            };
            EventDict["ya_yi_qi_fen"].options[0].results[1].wrapper.CanTrigger = delegate {
                return TreeItemDict["yin_yue_kuang_huan_jie"].wrapper.Unlocked;
            };

            // 犯罪气氛 > 警察机关免疫
            EventDict["fan_zui_qi_fen"].options[0].results[0].wrapper.CanTrigger = delegate {
                return !TreeItemDict["jing_cha_ji_guan"].wrapper.Unlocked;
            };
            EventDict["fan_zui_qi_fen"].options[0].results[1].wrapper.CanTrigger = delegate {
                return TreeItemDict["jing_cha_ji_guan"].wrapper.Unlocked;
            };

            // 机器过载
            EventDict["ji_qi_guo_zai"].options[0].wrapper.OnSelected = option => {
                // Map.dealDamage(type)
                // todo: 机器过载 > 破坏建筑
                Debug.LogWarning("[TODO] 机器过载 > 破坏建筑");
            };
            EventDict["ji_qi_guo_zai"].options[0].results[0].wrapper.CanTrigger = delegate {
                return !TreeItemDict["zhi_leng_qi"].wrapper.Unlocked;
            };
            EventDict["ji_qi_guo_zai"].options[0].results[1].wrapper.CanTrigger = delegate {
                return TreeItemDict["zhi_leng_qi"].wrapper.Unlocked;
            };

            // 官僚体系
            EventDict["guan_liao_ti_xi"].options[0].results[0].wrapper.CanTrigger = delegate {
                return !TreeItemDict["zhong_yang_wei_yuan_hui"].wrapper.Unlocked;
            };
            EventDict["guan_liao_ti_xi"].options[0].results[1].wrapper.CanTrigger = delegate {
                return TreeItemDict["zhong_yang_wei_yuan_hui"].wrapper.Unlocked;
            };

            // 住宅倒塌
            // rst0: 钢结构 rst1: 砖混结构; rst2: 最坏; 
            EventDict["zhu_zhai_dao_ta"].options[0].results[0].wrapper.CanTrigger =
                delegate { return TreeItemDict["gang_jie_gou"].wrapper.Unlocked; };
            EventDict["zhu_zhai_dao_ta"].options[0].results[1].wrapper.CanTrigger =
                delegate {
                    return /*!TreeItemDict["gang_jie_gou"].wrapper.Unlocked &&*/
                        TreeItemDict["zhuan_hun_jie_gou"].wrapper.Unlocked;
                };

            // 罢工
            EventDict["ba_gong"].options[2].wrapper.CanUnlock =
                option => TreeItemDict["zu_zhi_wei_yuan_hui"].wrapper.Unlocked;
            // switch to ba_gong2
            // todo: add event trigger callback
            EventDict["ba_gong"].options[0].wrapper.OnSelected = option => {
                EventDict["ba_gong"].wrapper.Probability = 0;
                EventDict["ba_gong2"].wrapper.Probability = EventDict["ba_gong"].initProbability;
            };
            EventDict["ba_gong"].options[1].wrapper.OnSelected = option => {
                EventDict["ba_gong"].wrapper.Probability = 0;
                EventDict["ba_gong2"].wrapper.Probability = EventDict["ba_gong"].initProbability;
            };
            EventDict["ba_gong"].options[2].wrapper.OnSelected = option => {
                EventDict["ba_gong"].wrapper.Probability = 0;
                EventDict["ba_gong2"].wrapper.Probability = EventDict["ba_gong"].initProbability;
            };
            // 罢工2
            EventDict["ba_gong2"].wrapper.Probability = 0;
            EventDict["ba_gong2"].options[2].wrapper.CanUnlock =
                option => TreeItemDict["zu_zhi_wei_yuan_hui"].wrapper.Unlocked;


            // 故事：复苏的火种
            EventDict["fu_su_de_huo_zhong"].options[0].results[0].wrapper.OnTrigger = result => {
                result.EventSource.wrapper.Probability = 0;
            };

            // 故事：离去
            EventDict["li_qu"].options[0].results[0].wrapper.OnTrigger = result => {
                SceneObjRef.Instance.EventUI.ForceEndProcessAndStartNewRound();
            };
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
            // ---- Tech ----
            // lv1
            TreeItemDict["sun_mao_jie_gou"].wrapper.onUnlocked = delegate {
                BuildingDict["zhu_fang_lv2"].TryUnlock();
                BuildingLevels.Instance.Upgrade(PlayerBuildingType.ZhuFang);
            };
            TreeItemDict["geng_li"].wrapper.onUnlocked = delegate { BuildingDict["nong_tian_lv1"].TryUnlock(); };
            TreeItemDict["cha_yu_yan"].wrapper.onUnlocked = delegate {
                var delta = PropertyManager.Instance.GameProp[PropertyType.PopulationDelta] * .2f;
                var prop = new PropertyRepr(PropertyType.PopulationDelta, (int)delta);
                PropertyManager.Instance.AddProperty(prop);
            };
            TreeItemDict["ji_chu_ji_xie"].wrapper.onUnlocked = delegate { BuildingDict["kuang_jing_lv1"].TryUnlock(); };
            // lv2
            TreeItemDict["zhuan_hun_jie_gou"].wrapper.onUnlocked = delegate {
                BuildingDict["zhu_fang_lv3"].TryUnlock();
                BuildingLevels.Instance.Upgrade(PlayerBuildingType.ZhuFang);
            };
            TreeItemDict["cha_yang"].wrapper.onUnlocked = delegate {
                BuildingDict["nong_tian_lv2"].TryUnlock();
                BuildingLevels.Instance.Upgrade(PlayerBuildingType.NongTian);
            };
            // lv3
            TreeItemDict["gang_hun_jie_gou"].wrapper.onUnlocked = delegate {
                BuildingDict["zhu_fang_lv4"].TryUnlock();
                BuildingLevels.Instance.Upgrade(PlayerBuildingType.ZhuFang);
            };
            TreeItemDict["li_ti_nong_ye"].wrapper.onUnlocked = delegate {
                BuildingDict["nong_tian_lv3"].TryUnlock();
                BuildingLevels.Instance.Upgrade(PlayerBuildingType.NongTian);
            };
            // lv4
            TreeItemDict["gang_jie_gou"].wrapper.onUnlocked = delegate {
                BuildingDict["zhu_fang_lv5"].TryUnlock();
                BuildingLevels.Instance.Upgrade(PlayerBuildingType.ZhuFang);
            };
            TreeItemDict["da_xing_nong_yong_ji_xie"].wrapper.onUnlocked = delegate {
                BuildingDict["nong_tian_lv4"].TryUnlock();
                BuildingLevels.Instance.Upgrade(PlayerBuildingType.NongTian);
            };
            TreeItemDict["ci_li_xian_quan"].wrapper.onUnlocked = delegate { BuildingDict["fa_dian_zhan"].TryUnlock(); };
            // ---- Culture ----
            // lv1
            TreeItemDict["yin_you_ge_zhe"].wrapper.onUnlocked = delegate {
                var delta = PropertyManager.Instance.GameProp[PropertyType.PopulationDelta] * .2f;
                var prop = new PropertyRepr(PropertyType.PopulationDelta, (int)delta);
                PropertyManager.Instance.AddProperty(prop);
            };
            TreeItemDict["huo_bi"].wrapper.onUnlocked = delegate {
                var delta = PropertyManager.Instance.GameProp[PropertyType.FinanceDelta] * .3f;
                var prop = new PropertyRepr(PropertyType.FinanceDelta, (int)delta);
                PropertyManager.Instance.AddProperty(prop);
            };
            // lv2
            TreeItemDict["yun_dong_hui"].wrapper.onUnlocked = delegate {
                var delta = PropertyManager.Instance.GameProp[PropertyType.PopulationDelta] * .1f;
                var prop = new PropertyRepr(PropertyType.PopulationDelta, (int)delta);
                PropertyManager.Instance.AddProperty(prop);
            };
            TreeItemDict["xue_yuan"].wrapper.onUnlocked = delegate {
                var delta = PropertyManager.Instance.GameProp[PropertyType.FinanceDelta] * .1f;
                var prop = new PropertyRepr(PropertyType.FinanceDelta, (int)delta);
                PropertyManager.Instance.AddProperty(prop);
            };
            // lv3
            TreeItemDict["yin_yue_guang_chang"].wrapper.onUnlocked = delegate {
                var delta = PropertyManager.Instance.GameProp[PropertyType.PopulationDelta] * .1f;
                var prop = new PropertyRepr(PropertyType.PopulationDelta, (int)delta);
                PropertyManager.Instance.AddProperty(prop);
            };
            // lv4
            TreeItemDict["quan_min_kuang_huan"].wrapper.onUnlocked = delegate {
                var delta = PropertyManager.Instance.GameProp[PropertyType.PopulationDelta] * .1f;
                var prop = new PropertyRepr(PropertyType.PopulationDelta, (int)delta);
                PropertyManager.Instance.AddProperty(prop);
            };
            // lv5
            TreeItemDict["li_xiang_she_hui"].wrapper.onUnlocked = delegate {
                SceneObjRef.Instance.MainMenuCanvas.gameObject.SetActive(true);
            };
        }
    }
}
