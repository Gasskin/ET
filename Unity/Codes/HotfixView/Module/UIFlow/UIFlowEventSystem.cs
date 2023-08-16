using System;

namespace ET
{
    [FriendClass(typeof(UIFlowEventComponent))]
    public static class UIFlowEventSystem
    {
        public class OnAwake : AwakeSystem<UIFlowEventComponent>
        {
            public override void Awake(UIFlowEventComponent self)
            {
                UIFlowEventComponent.Instance = self;
                foreach (var type in Game.EventSystem.GetTypes(typeof(UIFlowEventAttribute)))
                {
                    var attr = type.GetCustomAttributes(typeof (UIFlowEventAttribute), false)[0] as UIFlowEventAttribute;
                    self.EventHandlers.Add(attr.WindowID, Activator.CreateInstance(type) as IUIFlowEventHandler);
                }
            }
        }
        
        public class OnDestroy: DestroySystem<UIFlowEventComponent>
        {
            public override void Destroy(UIFlowEventComponent self)
            {
                self.EventHandlers.Clear();
                UIFlowEventComponent.Instance = null;
            }
        }

        public static IUIFlowEventHandler GetUIFlowEventHandler(this UIFlowEventComponent self, WindowID windowID)
        {
            if (self.EventHandlers.TryGetValue(windowID,out var handler))
            {
                return handler;
            }

            Log.Error($"找不到EventHandler:{windowID}");
            return null;
        }
    }
}