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
        /// 窗口是否是正在显示的 
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        /// <returns></returns>
        public static bool IsWindowVisible(this UIFlowComponent self, WindowID id)
        {
            return self.VisibleWindowsDic.ContainsKey(id);
        }

        /// <summary>
        /// 关闭所有窗口
        /// </summary>
        /// <param name="self"></param>
        public static void CloseAllWindow(this UIFlowComponent self)
        {
        }
        
        /// <summary>
        /// 根据指定Id的显示UI窗口
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        /// <OtherParam name="showData"></OtherParam>
        public static void OpenWindowAsync(this UIComponent self,WindowID id, Object showData = null)
        {
      
        }
    #endregion
    }
}