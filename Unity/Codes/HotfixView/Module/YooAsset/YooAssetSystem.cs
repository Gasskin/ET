using System;
using YooAsset;

namespace ET
{
    [FriendClass(typeof (YooAssetsComponent))]
    public static class YooAssetSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<YooAssetsComponent, string>
        {
            public override void Awake(YooAssetsComponent self, string a)
            {
                YooAssetsComponent.Instance = self;
                self.packageName = a;
                YooAssets.Initialize();
                var package = YooAssets.CreatePackage(self.packageName);
                YooAssets.SetDefaultPackage(package);
                self.resourcePackage = package;
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<YooAssetsComponent>
        {
            public override void Destroy(YooAssetsComponent self)
            {
            }
        }

        public static async ETTask InitializeAsync(this YooAssetsComponent self)
        {
            CoroutineLock coroLock = null;
            try
            {
                coroLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.YooAsset, -1);
                var initParameters = new EditorSimulateModeParameters();
                var simulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild(EDefaultBuildPipeline.BuiltinBuildPipeline, self.packageName);
                initParameters.SimulateManifestFilePath = simulateManifestFilePath;
                var handle = self.resourcePackage.InitializeAsync(initParameters);
                var tcs = ETTask.Create(true);
                handle.Completed += (operate => { tcs.SetResult(); });
                await tcs;
                Log.Info("==================== YooAsset初始化完成 ====================");
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
            finally
            {
                coroLock?.Dispose();
            }

            await ETTask.CompletedTask;
        }
    }
}