using System;
using ET.UIFlow;
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
                var root = GameObject.FindWithTag("UIRoot").transform;
                if (root == null)
                {
                    Log.Error("找不到UIRoot");
                    return;
                }

                foreach (var layerName in Enum.GetNames(typeof (WindowLayer)))
                {
                    if (layerName == "None")
                        continue;
                    var layer = root.Find(layerName);
                    if (layer == null)
                    {
                        Log.Error($"找不到Layer: {layerName}");
                        self.LayerDic.Clear();
                        return;
                    }

                    var type = Enum.Parse<WindowLayer>(layerName);
                    self.LayerDic[type] = layer;
                }

                self.UIRoot = root;
                UnityEngine.Object.DontDestroyOnLoad(self.UIRoot);

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
                    self.AllWindowsDic[id] = wnd;
                    await self.LoadAsset(wnd);
                }

                if (wnd.Prefab == null)
                {
                    wnd.Dispose();
                    self.AllWindowsDic.Remove(id);
                    return;
                }

                wnd.Prefab.SetActive(true);
                UIFlowEventComponent.Instance.GetUIFlowEventHandler(id).OnShow(wnd);
                self.VisibleWindowsDic[id] = wnd;
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

        public static void CloseWindow(this UIFlowComponent self, WindowID id)
        {
            
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
            var path = UIFlowConfigComponent.Instance.GetWindowPrefabPath(wnd.WindowID);
            var layer = UIFlowConfigComponent.Instance.GetWindowLayer(wnd.WindowID);
            if (string.IsNullOrEmpty(path))
            {
                Log.Error($"不存在Prefab路径：{wnd.WindowID}");
                return;
            }

            if (layer == WindowLayer.None)
            {
                Log.Error($"WindowLayer配置错误：{wnd.WindowID}");
                return;
            }

            var asset = await AssetComponent.Instance.GetAsset(path);
            var go = UnityEngine.Object.Instantiate(asset, self.LayerDic[layer], false) as GameObject;
            wnd.Prefab = go;
            wnd.Rect = go.GetComponent<RectTransform>();
            UIFlowEventComponent.Instance.GetUIFlowEventHandler(wnd.WindowID).OnLoad(wnd);
            await ETTask.CompletedTask;
        }
    #endregion
    }
}