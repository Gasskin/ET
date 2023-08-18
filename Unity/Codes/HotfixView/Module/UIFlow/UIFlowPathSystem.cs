namespace ET
{
    [FriendClass(typeof(UIFlowPathComponent))]
    public static class UIPathSystem
    {
        [ObjectSystem]
        public class OnAwake: AwakeSystem<UIFlowPathComponent>
        {
            public override void Awake(UIFlowPathComponent self)
            {
                self.PrefabPath.Add(WindowID.LoginNew, "Login/UILoginNew");

                UIFlowPathComponent.Instance = self;
            }
        }
        
        [ObjectSystem]
        public class OnDestroy: DestroySystem<UIFlowPathComponent>
        {
            public override void Destroy(UIFlowPathComponent self)
            {
                self.PrefabPath.Clear();
                UIFlowPathComponent.Instance = null;
            }
        }

        public static string GetWindowPrefabPath(this UIFlowPathComponent self, WindowID id)
        {
            if (self.PrefabPath.TryGetValue(id,out var path))
            {
                return path;
            }

            return null;
        }
    }
}