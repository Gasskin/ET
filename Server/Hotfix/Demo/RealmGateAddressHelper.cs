using System.Collections.Generic;

namespace ET
{
    public static class RealmGateAddressHelper
    {
        // public static StartSceneConfig GetGate(int zone)
        // {
        //     List<StartSceneConfig> zoneGates = StartSceneConfigCategory.Instance.Gates[zone];
        //
        //     int n = RandomHelper.RandomNumber(0, zoneGates.Count);
        //
        //     return zoneGates[n];
        // }

        public static cfg.StartSceneConfig GetGate(int zone,long accountId)
        {
            var gates = new List<cfg.StartSceneConfig>();
            foreach (var config in LuBanComponent.Instance.GetAllTable().StartSceneTable.DataList)
            {
                if (config.SceneType == cfg.Enum.SceneType.Gate && config.StartZoneConfig == zone)
                    gates.Add(config);
            }

            return gates[accountId.GetHashCode() % gates.Count];
        }
    }
}