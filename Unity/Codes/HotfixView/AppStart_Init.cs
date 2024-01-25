using UnityEngine;
using YooAsset;

namespace ET
{
    public class AppStart_Init: AEvent<EventType.AppStart>
    {
        protected override void Run(EventType.AppStart args)
        {
            RunAsync(args).Coroutine();
        }
        
        private async ETTask RunAsync(EventType.AppStart args)
        {
            Game.Scene.AddComponent<TimerComponent>();
            Game.Scene.AddComponent<CoroutineLockComponent>();

            // 加载配置
            Game.Scene.AddComponent<ResourcesComponent>();
            await ResourcesComponent.Instance.LoadBundleAsync("config.unity3d");
            Game.Scene.AddComponent<ConfigComponent>();
            ConfigComponent.Instance.Load();
            ResourcesComponent.Instance.UnloadBundle("config.unity3d");
            
            Game.Scene.AddComponent<OpcodeTypeComponent>();
            Game.Scene.AddComponent<MessageDispatcherComponent>();
            
            Game.Scene.AddComponent<NetThreadComponent>();
            Game.Scene.AddComponent<SessionStreamDispatcher>();
            Game.Scene.AddComponent<ZoneSceneManagerComponent>();
            
            Game.Scene.AddComponent<GlobalComponent>();
            Game.Scene.AddComponent<NumericWatcherComponent>();
            Game.Scene.AddComponent<AIDispatcherComponent>();

            Game.Scene.AddComponent<LuBanComponent>();
            Game.Scene.AddComponent<AssetComponent>();
            Game.Scene.AddComponent<UIFlowComponent>();
            Game.Scene.AddComponent<UIFlowConfigComponent>();
            Game.Scene.AddComponent<UIFlowEventComponent>();

            await ResourcesComponent.Instance.LoadBundleAsync("unit.unity3d");

            Game.Scene.AddComponent<YooAssetsComponent, string>("AssetsPackage");
            await YooAssetsComponent.Instance.InitializeAsync();
            
            Scene zoneScene = SceneFactory.CreateZoneScene(1, "Game", Game.Scene);
            
            Game.EventSystem.Publish(new EventType.AppStartInitFinish() { ZoneScene = zoneScene });

            var handle = YooAssets.LoadAssetAsync<GameObject>("Assets/AssetsPackage/UI/Prefab/Login/UILoginNew.prefab");
            var tcs = ETTask<GameObject>.Create(true);
            handle.Completed += operate => { tcs.SetResult(handle.AssetObject as GameObject);};
            var go = await tcs;
            GameObject.Instantiate(go);
            handle.Release();
        }
    }
}
