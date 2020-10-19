using System;
using Building;
using Map;
using Property;
using State;
using Tree;
using Turn;
using Turn.Buff;
using UnityEngine;

namespace Util {
    [DefaultExecutionOrder(-20)]
    public class SingleObjRef : SingletonMono<SingleObjRef> {
        public BuffQueue BuffQueueInstance { get; private set; }

        public NameidSobjDict<BuildingSobj> BuildingDict { get; private set; }
        public NameidSobjDict<TreeItemSobj> TreeItemDict { get; private set; }

        public PropertyManager PropertyManagerInstance { get; private set; }

        public TurnCounter TurnCounterInstance { get; private set; }
        public StateManager StateManagerInstance { get; private set; }

        public MapManager MapManagerInstance { get; private set; }

        protected override void OnInstanceAwake() {
            BuffQueueInstance = new BuffQueue();
            BuffQueueInstance.Clear();

            InitBuildingSobjs();
            InitTreeItemSobjs();

            PropertyManagerInstance = new PropertyManager();
            PropertyManagerInstance.Reset();

            TurnCounterInstance = new TurnCounter();
            TurnCounterInstance.Reset();
            TurnCounterInstance.OnNewTurn += BuffQueueInstance.OnNewTurn;

            StateManagerInstance = new StateManager();
            StateManagerInstance.Reset();

            MapManagerInstance = new MapManager(4, 5);
        }


        private void InitBuildingSobjs() {
            var sobjs = Resources.LoadAll<BuildingSobj>("Building");

            BuildingDict = new NameidSobjDict<BuildingSobj>(sobjs);
            foreach(var sobj in sobjs) {
                if(sobj.initAsLocked)
                    sobj.Lock();
                else
                    sobj.ForceUnlock();
            }
        }


        private void InitTreeItemSobjs() {
            var sobjs = Resources.LoadAll<TreeItemSobj>("TreeItemSobj");

            TreeItemDict = new NameidSobjDict<TreeItemSobj>(sobjs);
            foreach(var sobj in sobjs) {
                var holder = new TreeItemWrapper(sobj);
                holder.Lock();
            }

            // todo: check in themselves instead since TreeItem has state
            // lv1
            TreeItemDict["sun_mao_jie_gou"].wrapper.onUnlocked = _ => BuildingDict["mu_wu"].ForceUnlock();
            TreeItemDict["jian_zhu_xia_shui"].wrapper.onUnlocked =
                _ => throw new NotImplementedException("jian_zhu_xia_shui unlock callback");
            TreeItemDict["geng_li"].wrapper.onUnlocked = _ => BuildingDict["nong_tian"].ForceUnlock();
            TreeItemDict["cha_yu_yan"].wrapper.onUnlocked = _ => {
                var delta = PropertyManagerInstance.GameProp[PropertyType.PopulationDelta] * .2f;
                var prop = new PropertyRepr(PropertyType.PopulationDelta, (int)delta);
                PropertyManagerInstance.AddProperty(prop);
            };
            TreeItemDict["ji_chu_ji_xie"].wrapper.onUnlocked = _ => BuildingDict["kuang_jing"].ForceUnlock();
            TreeItemDict["mu_zhi_shui_ba"].wrapper.onUnlocked =
                _ => throw new NotImplementedException("mu_zhi_shui_ba unlock callback");
            TreeItemDict["chuan_tong_liao_fa"].wrapper.onUnlocked =
                _ => throw new NotImplementedException("chuan_tong_liao_fa unlock callback");
            // lv2
            TreeItemDict["zhuan_hun_jie_gou"].wrapper.onUnlocked = _ => BuildingDict["zhuan_lou"].ForceUnlock();
            TreeItemDict["jian_zhu_bao_wen"].wrapper.onUnlocked =
                _ => throw new NotImplementedException("jian_zhu_bao_wen unlock callback");
            TreeItemDict["cha_yang"].wrapper.onUnlocked = _ => BuildingDict["shui_tian"].ForceUnlock();
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
