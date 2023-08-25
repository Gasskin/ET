using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class UIFlowConfigComponent: Entity,IAwake,IDestroy
    {
        public static UIFlowConfigComponent Instance;
        public Dictionary<WindowID, string> PrefabPath { get; } = new();
        public Dictionary<WindowID, WindowLayer> WindowLayer { get; } = new();
    }
}