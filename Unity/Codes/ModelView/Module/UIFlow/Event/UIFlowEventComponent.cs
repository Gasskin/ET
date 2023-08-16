using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class UIFlowEventComponent: Entity,IAwake,IDestroy
    {
        public static UIFlowEventComponent Instance;
        public Dictionary<WindowID, IUIFlowEventHandler> EventHandlers { get; } = new();
    }
}