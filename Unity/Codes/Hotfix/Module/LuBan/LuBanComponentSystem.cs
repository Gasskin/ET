using System.IO;
using cfg;
using SimpleJSON;
using UnityEngine;

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
                        JSON.Parse(File.ReadAllText($"{Application.dataPath}/../Codes/Model/Generate/LuBan/Data/{file}.json")));
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