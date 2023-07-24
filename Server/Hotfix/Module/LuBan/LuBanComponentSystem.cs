using System;
using System.Text.Json;
using cfg;

namespace ET
{
    [FriendClass(typeof (LuBanComponent))]
    public static class LuBanComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<LuBanComponent>
        {
            public override void Awake(LuBanComponent self)
            {
                LuBanComponent.Instance = self;
                self.tables = new Tables(file =>
                        JsonDocument.Parse(System.IO.File.ReadAllBytes($"../Server/Model/Generate/LuBan/Data/{file}.json")).RootElement);
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<LuBanComponent>
        {
            public override void Destroy(LuBanComponent self)
            {
                LuBanComponent.Instance = null;
                self.tables = null;
            }
        }

        public static Tables GetAllTable(this LuBanComponent self)
        {
            return self.tables;
        }

    #region StartScene

        public static cfg.StartSceneConfig GetLocationConfig(this LuBanComponent self)
        {
            foreach (var config in self.tables.StartSceneTable.DataList)
            {
                if (config.SceneType == cfg.Enum.SceneType.Location)
                {
                    return config;
                }
            }

            return null;
        }

        public static cfg.StartSceneConfig GetBySceneName(this LuBanComponent self, int zone, string name)
        {
            foreach (var config in self.tables.StartSceneTable.DataList)
            {
                if (config.StartZoneConfig == zone && config.Name == name) 
                {
                    return config;
                }
            }
            return null;
        }
    #endregion
    }
}