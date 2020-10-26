using System.Collections.Generic;
using Util;

namespace Building {
    /// <summary> Init this class AFTER BuildingDict is built </summary>
    public class BuildingLevels : ManualSingleton<BuildingLevels> {
        public BuildingLevels(NameidSobjDict<BuildingDescription> buildDict) {
            upgradeChain = new Dictionary<PlayerBuildingType, BuildingDescription> {
                [PlayerBuildingType.ZhuJiDi] = buildDict["zhu_ji_di_lv1"],
                [PlayerBuildingType.ZhuFang] = buildDict["zhu_fang_lv1"],
                [PlayerBuildingType.CaiJiZhan] = buildDict["cai_ji_zhan"],
                [PlayerBuildingType.NongTian] = buildDict["nong_tian_lv1"],
                [PlayerBuildingType.KuangJing] = buildDict["kuang_jing_lv1"],
                [PlayerBuildingType.KeYanJianZhu] = buildDict["ke_yan_jian_zhu_lv1"],
                [PlayerBuildingType.FaDianZhan] = buildDict["fa_dian_zhan"]
            };
        }

        //public int GetCurrLevel(BuildingType type) {
        //    var sobj = upgradeChain[type];
        //    if(sobj.Unlocked)
        //        return sobj.level;
        //    return sobj.level - 1;
        //}

        public BuildingDescription GetCurrLevel(PlayerBuildingType type) {
            return upgradeChain[type];
        }

        public bool HasNextLevel(PlayerBuildingType type) {
            return upgradeChain[type].nextLevel != null;
        }

        /// <returns> The previous level </returns>
        public BuildingDescription Upgrade(PlayerBuildingType type) {
            var prev = upgradeChain[type];
            upgradeChain[type] = prev.nextLevel;
            return prev;
        }

        private readonly Dictionary<PlayerBuildingType, BuildingDescription> upgradeChain;


        public override void OnReset() {
            var buildDict = SobjRef.Instance.BuildingDict;
            upgradeChain[PlayerBuildingType.ZhuJiDi] = buildDict["zhu_ji_di_lv1"];
            upgradeChain[PlayerBuildingType.ZhuFang] = buildDict["zhu_fang_lv1"];
            upgradeChain[PlayerBuildingType.CaiJiZhan] = buildDict["cai_ji_zhan"];
            upgradeChain[PlayerBuildingType.NongTian] = buildDict["nong_tian_lv1"];
            upgradeChain[PlayerBuildingType.KuangJing] = buildDict["kuang_jing_lv1"];
            upgradeChain[PlayerBuildingType.KeYanJianZhu] = buildDict["ke_yan_jian_zhu_lv1"];
            upgradeChain[PlayerBuildingType.FaDianZhan] = buildDict["fa_dian_zhan"];
        }
    }


    public enum PlayerBuildingType {
        ZhuJiDi = 0, ZhuFang, CaiJiZhan, NongTian, KuangJing, KeYanJianZhu, FaDianZhan,
        Other
    }
}
