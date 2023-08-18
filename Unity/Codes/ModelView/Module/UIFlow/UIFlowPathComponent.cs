using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class UIFlowPathComponent: Entity,IAwake,IDestroy
    {
        public static UIFlowPathComponent Instance;
        public Dictionary<WindowID, string> PrefabPath { get; } = new();
    }
}