using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ET
{
    [FriendClass(typeof (AssetComponent))]
    public static class AssetComponentSystem
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

        public static async UniTask<T> LoadAsset<T>(this AssetComponent self, string path) where T : UnityEngine.Object
        {
            if (self.assetCache.TryGetValue(path,out var asset))
                return asset as T;
            asset = await Resources.LoadAsync(path);
            if (asset == null)
                return null;
            self.assetCache.Add(path, asset);
            return asset as T;
        }

        public static void UnLoadAsset(this AssetComponent self, string path)
        {
            
        }
    #endregion
    }
}