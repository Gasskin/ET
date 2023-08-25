namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class AccountInfoComponent: Entity, IAwake, IDestroy
    {
        public static AccountInfoComponent Instance;
        public string Token { get; set; }
        public long AccountId { get; set; }
    }
}