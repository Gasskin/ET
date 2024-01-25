
namespace ET
{
    [FriendClass(typeof(YooAssetComponent))]
    public static class YooAssetSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<YooAssetComponent,string>
        {
            public override void Awake(YooAssetComponent self, string a)
            {
                self.packageName = a;
#if UNITY_EDITOR
#else
#endif
            }
        }
        
        [ObjectSystem]
        public class DestroySystem: DestroySystem<YooAssetComponent>
        {
            public override void Destroy(YooAssetComponent self)
            {
                
            }
        }
    }
}