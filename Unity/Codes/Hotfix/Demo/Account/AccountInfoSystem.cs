using System;

namespace ET
{
    [FriendClass(typeof(AccountInfoComponent))]
    public static class AccountInfoSystem
    {
        public class OnAwake: AwakeSystem<AccountInfoComponent>
        {
            public override void Awake(AccountInfoComponent self)
            {
                AccountInfoComponent.Instance = self;
            }
        }

        public class OnDestroy: DestroySystem<AccountInfoComponent>
        {
            public override void Destroy(AccountInfoComponent self)
            {
                self.Token = String.Empty;
                self.AccountId = 0;
                AccountInfoComponent.Instance = null;
            }
        }

        public static void SetAccountInfo(this AccountInfoComponent self, string token, long accountId)
        {
            self.Token = token;
            self.AccountId = accountId;
        }
    }
}