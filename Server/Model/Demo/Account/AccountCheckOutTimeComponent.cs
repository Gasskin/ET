namespace ET
{
    [ComponentOf(typeof(Session))]
    public class AccountCheckOutTimeComponent: Entity,IAwake<long>,IDestroy, IAwake
    {
        public long timer;
        public long accountId;
    }
}