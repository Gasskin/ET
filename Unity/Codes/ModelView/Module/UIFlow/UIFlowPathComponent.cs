using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class UIPathComponent: Entity,IAwake,IDestroy
    {
        public static UIPathComponent Instance;
        public Dictionary<WindowID, string> PrefabPath { get; } = new();
    }
}