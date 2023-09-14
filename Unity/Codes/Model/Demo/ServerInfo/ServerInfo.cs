namespace ET
{
    public enum ServerStatus
    {
        Normal = 0,
        Close = 1,
    }
    
    public class ServerInfo: Entity,IAwake
    {
        public int status;
        public string serverName;
    }
}