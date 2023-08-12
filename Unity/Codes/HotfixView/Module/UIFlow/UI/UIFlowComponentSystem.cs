using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ET.UIFlow
{
    [FriendClass(typeof (UIFlowComponent))]
    public static class UIFlowComponentSystem
    {
    #region 生命周期

        public class OnAwake: AwakeSystem<UIFlowComponent>
        {
            public override async void Awake(UIFlowComponent self)
            {
                if (self.isInit)
                    return;
                self.config = await Resources.LoadAsync<UIConfig>("UIConfig") as UIConfig;
                if (self.config == null)
                {
                    Log.Error("UIFlowConfig为Null");
                    return;
                }

                var uiRootAsset = await Resources.LoadAsync<GameObject>("UIRoot") as GameObject;
                if (uiRootAsset == null)
                {
                    Log.Error("找不到 UIRoot");
                    return;
                }

                self.uiRoot = UnityEngine.Object.Instantiate(uiRootAsset);
                self.uiRoot.name = "UIRoot";
                UnityEngine.Object.DontDestroyOnLoad(self.uiRoot);

                self.uiLayer = new Dictionary<UIType, Transform>();
                self.uiLogics = new Dictionary<string, UIBase>();
                self.uiPrefabAssets = new Dictionary<string, GameObject>();
                self.uiWaitForUnLoad = new Dictionary<string, UIBase>();
                self.uiUnLoadCountDown = new Dictionary<string, float>();
                self.removeHelper = new List<string>();

                foreach (var type in Enum.GetValues(typeof (UIType)))
                {
                    var layer = self.uiRoot.transform.Find(type.ToString());
                    if (layer != null)
                        self.uiLayer.Add((UIType)type, layer);
                    else
                        Log.Error($"找不到层级：{type}");
                }

                self.isInit = true;
            }
        }

        public class OnUpdate: UpdateSystem<UIFlowComponent>
        {
            public override void Update(UIFlowComponent self)
            {
                if (!self.isInit)
                    return;

                foreach (var uiLogic in self.uiLogics.Values)
                {
                    if (uiLogic.IsOpen)
                    {
                        uiLogic.Update();
                    }
                }

                self.removeHelper.Clear();
                var currentTime = Time.realtimeSinceStartup;
                foreach (var countDownPair in self.uiUnLoadCountDown)
                {
                    if (currentTime - countDownPair.Value >= self.config.unLoadTime)
                    {
                        self.removeHelper.Add(countDownPair.Key);
                    }
                }

                foreach (var uiName in self.removeHelper)
                {
                    if (self.uiWaitForUnLoad.TryGetValue(uiName, out var logic))
                    {
                        logic.Unload();
                        AssetComponent.Instance.UnLoadAsset(logic.PrefabName);
                    }

                    self.uiWaitForUnLoad.Remove(uiName);
                    self.uiUnLoadCountDown.Remove(uiName);
                }
            }
        }

        public class OnDestroy: DestroySystem<UIFlowComponent>
        {
            public override void Destroy(UIFlowComponent self)
            {
            }
        }

    #endregion

    #region 接口方法

        public static void Open<T>(this UIFlowComponent self) where T : UIBase, new()
        {
            if (!self.isInit)
                return;
            
            var uiName = typeof (T).Name;
            if (self.uiLogics.ContainsKey(uiName))
            {
                Log.Error($"UI：{uiName} 已经打开");
                return;
            }

            // 是否在卸载列表里
            if (self.uiWaitForUnLoad.TryGetValue(uiName, out var uiLogic))
            {
                self.InternalOpen(uiName, uiLogic);
                return;
            }

            self.CreateUI<T>(uiName);
        }

        public static void Close<T>(this UIFlowComponent self) where T : UIBase, new()
        {
            if (!self.isInit)
                return;

            var uiName = typeof (T).Name;
            if (!self.uiLogics.TryGetValue(uiName, out var uiLogic))
            {
                Debug.LogError($"关闭了一个没有打开的UI：{uiName}");
                return;
            }

            self.InternalClose(uiName, uiLogic);
        }

    #endregion

    #region 工具方法

        private static void InternalOpen(this UIFlowComponent self, string uiName, UIBase uiLogic)
        {
            self.uiWaitForUnLoad.Remove(uiName);
            self.uiUnLoadCountDown.Remove(uiName);
            self.uiLogics.Add(uiName, uiLogic);
            uiLogic.Show();
        }

        private static void InternalClose(this UIFlowComponent self, string uiName, UIBase uiLogic)
        {
            uiLogic.Close();
            self.uiLogics.Remove(uiName);
            self.uiWaitForUnLoad.Add(uiLogic.PrefabName, uiLogic);
            self.uiUnLoadCountDown.Add(uiLogic.PrefabName, Time.realtimeSinceStartup);
        }

        private static async void CreateUI<T>(this UIFlowComponent self, string uiName) where T : UIBase, new()
        {
            var uiLogic = new T();
            if (!self.uiLayer.TryGetValue(uiLogic.Layer, out var layer))
            {
                Log.Error($"找不到UI：{uiName} 的目标层级：{uiLogic.Layer}");
                return;
            }

            var asset = await AssetComponent.Instance.LoadAsset<GameObject>(uiLogic.PrefabName);
            if (asset == null)
                return;

            var instance = UnityEngine.Object.Instantiate(asset, layer);
            if (!uiLogic.BindComponent(instance))
            {
                Log.Error($"{uiName} BindComponent Error!");
                UnityEngine.Object.DestroyImmediate(instance);
                AssetComponent.Instance.UnLoadAsset(uiLogic.PrefabName);
                return;
            }

            uiLogic.Load();
            self.InternalOpen(uiName, uiLogic);
        }

    #endregion
    }
}