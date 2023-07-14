using System.Text.Json;
using cfg;

namespace ET
{
    [FriendClass(typeof(LuBanComponent))]
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

        public static Tables Tables => LuBanComponent.Instance.tables;
    }
}