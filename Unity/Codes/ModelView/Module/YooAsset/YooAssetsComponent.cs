using YooAsset;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class YooAssetsComponent: Entity,IAwake<string>,IDestroy, IAwake
    {
        public static YooAssetsComponent Instance;
        public string packageName;
        public ResourcePackage resourcePackage;
    }
}
