using System.Collections.Generic;

namespace ET
{
    public class UIFlowEventComponent: Entity,IAwake,IDestroy
    {
        public static UIFlowEventComponent Instance;
        public Dictionary<WindowID, IUIFlowEventHandler> EventHandlers { get; } = new();
    }
}