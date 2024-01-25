namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class YooAssetComponent: Entity,IAwake<string>,IDestroy, IAwake
    {
        public static YooAssetComponent Instance;
        public string packageName;
    }
}
