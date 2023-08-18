using System;
using UnityEngine;

namespace ET
{
    [FriendClass(typeof (AssetComponent))]
    public static class AssetSystem
    {
    #region 生命周期
        public class OnAwake: AwakeSystem<AssetComponent>
        {
            public override void Awake(AssetComponent self)
            {
                AssetComponent.Instance = self;
            }
        }

        public class OnUpdate: UpdateSystem<AssetComponent>
        {
            public override void Update(AssetComponent self)
            {
            }
        }

        public class OnDestroy: DestroySystem<AssetComponent>
        {
            public override void Destroy(AssetComponent self)
            {
                AssetComponent.Instance = null;
            }
        }
    #endregion

    #region 接口方法
        public static async ETTask<UnityEngine.Object> GetAsset(this AssetComponent self, string path)
        {
            CoroutineLock coroutineLock = null;
            try
            {
                coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.Resources, path.GetHashCode());
                
                return null;
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
            finally
            {
                coroutineLock?.Dispose();
            }
        }
    #endregion

    #region 工具方法
        private static async ETTask<UnityEngine.Object> LoadAsset(this AssetComponent self, string path)
        {
            if (self.assetCache.TryGetValue(path, out var asset))
                return asset;
            var tcs = ETTask<UnityEngine.Object>.Create(true);
            var request = Resources.LoadAsync(path);
            request.completed += operation => { tcs.SetResult(request.asset);};
            return await tcs;
        }
    #endregion
    }
}