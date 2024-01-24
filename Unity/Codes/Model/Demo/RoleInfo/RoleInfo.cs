namespace ET
{
    public enum RoleInfoState
    {
        Normal = 0,
        Freeze = 1,
    }
    
    public class RoleInfo: Entity,IAwake
    {
        public string name;
        public int serverId;
        public int state;
        public long accountId;
        public long lastLoginTime;
        public long createTime;
    }
}