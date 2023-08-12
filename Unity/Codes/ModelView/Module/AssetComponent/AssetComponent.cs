using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class AssetComponent: Entity,IAwake,IDestroy,IUpdate
    {
        public static AssetComponent Instance;
        public Dictionary<string, UnityEngine.Object> assetCache = new();
    }
}