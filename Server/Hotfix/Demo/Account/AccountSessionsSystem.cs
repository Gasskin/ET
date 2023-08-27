namespace ET
{
    public static class AccountSessionsSystem
    {
        public class OnDestroy: DestroySystem<AccountSessionsComponent>
        {
            public override void Destroy(AccountSessionsComponent self)
            {
                self.AccountSessionsDic.Clear();
            }
        }

        public static long Get(this AccountSessionsComponent self, long accountId)
        {
            return self.AccountSessionsDic.TryGetValue(accountId, out var value)? value : 0;
        }

        public static void Add(this AccountSessionsComponent self, long accoutnId, long sessionId)
        {
            if (self.AccountSessionsDic.ContainsKey(accoutnId))
                self.AccountSessionsDic[accoutnId] = sessionId;
            else
                self.AccountSessionsDic.Add(accoutnId,sessionId);
        }

        public static void Remove(this AccountSessionsComponent self, long accountId)
        {
            self.AccountSessionsDic.Remove(accountId);
        }
    }
}