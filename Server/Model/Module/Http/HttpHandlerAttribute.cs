namespace ET
{
    public class HttpHandlerAttribute: BaseAttribute
    {
        public cfg.Enum.SceneType SceneType { get; }

        public string Path { get; }

        public HttpHandlerAttribute(cfg.Enum.SceneType sceneType, string path)
        {
            this.SceneType = sceneType;
            this.Path = path;
        }
    }
}