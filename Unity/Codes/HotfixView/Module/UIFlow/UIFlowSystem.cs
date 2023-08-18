using System;
using UnityEngine;

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

                UIFlowComponent.Instance = self;
            }
        }

        public class OnDestroy: DestroySystem<UIFlowComponent>
        {
            public override void Destroy(UIFlowComponent self)
            {
                UIFlowComponent.Instance = null;
            }
        }
    #endregion

    #region 接口方法
        /// <summary>
        /// 根据指定Id的显示UI窗口
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        /// <OtherParam name="showData"></OtherParam>
        public static async void OpenWindowAsync(this UIFlowComponent self, WindowID id, Object showData = null)
        {
            CoroutineLock coroutineLock = null;
            try
            {
                coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.OpenWindow, (long)id);
                var wnd = self.GetWindow(id);
                if (wnd == null)
                {
                    wnd = self.AddChild<UIFlowWindowComponent>();
                    wnd.WindowID = id;
                    await self.LoadAsset(wnd);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            finally
            {
                coroutineLock?.Dispose();
            }
        }
    #endregion

    #region 工具方法
        private static UIFlowWindowComponent GetWindow(this UIFlowComponent self, WindowID id)
        {
            if (self.AllWindowsDic.TryGetValue(id, out var wnd))
                return wnd;
            return null;
        }

        private static async ETTask LoadAsset(this UIFlowComponent self, UIFlowWindowComponent wnd)
        {
            var path = UIFlowPathComponent.Instance.GetWindowPrefabPath(wnd.WindowID);
            if (string.IsNullOrEmpty(path))
            {
                Log.Error($"不存在Prefab路径：{wnd.WindowID}");
                return;
            }
            var asset = await AssetComponent.Instance.GetAsset(path);
            var go = UnityEngine.Object.Instantiate(asset) as GameObject;
            wnd.Prefab = go;
            UIFlowEventComponent.Instance.GetUIFlowEventHandler(wnd.WindowID).OnLoad();
            await ETTask.CompletedTask;
        }
    #endregion
    }
}