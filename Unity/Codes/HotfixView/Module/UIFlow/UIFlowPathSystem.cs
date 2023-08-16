namespace ET
{
    [FriendClass(typeof(UIPathComponent))]
    public static class UIPathSystem
    {
        [ObjectSystem]
        public class OnAwake: AwakeSystem<UIPathComponent>
        {
            public override void Awake(UIPathComponent self)
            {
                self.PrefabPath.Add(WindowID.LoginNew, "Login/LoginNew.prefab");
            }
        }
        
        [ObjectSystem]
        public class OnDestroy: DestroySystem<UIPathComponent>
        {
            public override void Destroy(UIPathComponent self)
            {
                self.PrefabPath.Clear();
                UIPathComponent.Instance = null;
            }
        }

        public static string GetWindowPrefabPath(this UIPathComponent self, WindowID windowID)
        {
            return self.PrefabPath[windowID];
        }
    }
}