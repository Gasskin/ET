namespace ET
{
    public enum AccountType
    {
        General = 0,
        BlackList = 1,
    }

    public class Account: Entity, IAwake
    {
        public string accountName;
        public string password;
        public long createTime;
        public int accountType;
    }
}