namespace ET
{
    [FriendClass(typeof(UIFlowConfigComponent))]
    public static class UIFlowConfigSystem
    {
        [ObjectSystem]
        public class OnAwake: AwakeSystem<UIFlowConfigComponent>
        {
            public override void Awake(UIFlowConfigComponent self)
            {
                self.WindowLayer.Add(WindowID.LoginNew,WindowLayer.Normal);
                self.PrefabPath.Add(WindowID.LoginNew, "Login/UILoginNew");

                UIFlowConfigComponent.Instance = self;
            }
        }
        
        [ObjectSystem]
        public class OnDestroy: DestroySystem<UIFlowConfigComponent>
        {
            public override void Destroy(UIFlowConfigComponent self)
            {
                self.PrefabPath.Clear();
                UIFlowConfigComponent.Instance = null;
            }
        }

        public static string GetWindowPrefabPath(this UIFlowConfigComponent self, WindowID id)
        {
            if (self.PrefabPath.TryGetValue(id,out var path))
            {
                return path;
            }

            return null;
        }

        public static WindowLayer GetWindowLayer(this UIFlowConfigComponent self, WindowID id)
        {
            if (self.WindowLayer.TryGetValue(id,out var layer))
            {
                return layer;
            }

            return WindowLayer.None;
        }
    }
}