namespace ET
{
    [FriendClass(typeof (UIFlowComponent))]
    public static class UIFlowSystem
    {
    #region 生命周期
        public class OnAwake: AwakeSystem<UIFlowComponent>
        {
            public override void Awake(UIFlowComponent self)
            {
                self.AllWindowsDic ??= new();
                self.AllWindowsDic?.Clear();

                self.VisibleWindowsDic ??= new();
                self.AllWindowsDic?.Clear();

                self.WindowListCached ??= new();
                self.WindowListCached?.Clear();
            }
        }
        
        public class OnDestroy: DestroySystem<UIFlowComponent>
        {
            public override void Destroy(UIFlowComponent self)
            {
                self.CloseAllWindow();
            }
        }
    #endregion

    #region 接口方法
        /// <summary>
        /// 关闭所有窗口
        /// </summary>
        /// <param name="self"></param>
        public static void CloseAllWindow(this UIFlowComponent self)
        {
            
        }
    #endregion
    }
}