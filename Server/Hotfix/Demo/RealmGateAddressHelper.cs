using System.Collections.Generic;


namespace ET
{
	public static class RealmGateAddressHelper
	{
		// public static StartSceneConfig GetGate(int zone)
		// {
		// 	List<StartSceneConfig> zoneGates = StartSceneConfigCategory.Instance.Gates[zone];
		// 	
		// 	int n = RandomHelper.RandomNumber(0, zoneGates.Count);
		//
		// 	return zoneGates[n];
		// }

		public static cfg.StartSceneConfig GetGate(int zone)
		{
			var gates = new List<cfg.StartSceneConfig>();
			foreach (var config in LuBanComponent.Instance.GetAllTable().StartSceneTable.DataList)
			{
				if (config.SceneType == cfg.Enum.SceneType.Gate)
					gates.Add(config);
			}
			if (gates.Count > 0)
			{
				return gates[RandomHelper.RandomNumber(0, gates.Count)];
			}
			return null;
		}
	}
}
