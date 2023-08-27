using System;

namespace ET
{
    [FriendClass(typeof (AccountCheckOutTimeComponent))]
    public static class AccountCheckOutTimeSystem
    {
        [Timer(TimerType.AccountSessionCheckOutTime)]
        public class AccountSesstionCheckOutTimer: ATimer<AccountCheckOutTimeComponent>
        {
            public override void Run(AccountCheckOutTimeComponent t)
            {
                try
                {
                    t.DeleteSession();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    throw;
                }
            }
        }

        public class OnAwake: AwakeSystem<AccountCheckOutTimeComponent, long>
        {
            public override void Awake(AccountCheckOutTimeComponent self, long a)
            {
                self.accountId = a;
                TimerComponent.Instance.Remove(ref self.timer);
                self.timer = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + 600000, TimerType.AccountSessionCheckOutTime, self);
            }
        }

        public class OnDestroy: DestroySystem<AccountCheckOutTimeComponent>
        {
            public override void Destroy(AccountCheckOutTimeComponent self)
            {
                TimerComponent.Instance.Remove(ref self.timer);
            }
        }

        public static void DeleteSession(this AccountCheckOutTimeComponent self)
        {
            var session = self.GetParent<Session>();
            var sessionId = session.DomainScene().GetComponent<AccountSessionsComponent>().Get(self.accountId);
            if (session.InstanceId == sessionId)
                session.DomainScene().GetComponent<AccountSessionsComponent>().Remove(self.accountId);
            session.Send(new A2C_Disconnect() { Error = 1 });
            session.Disconnect().Coroutine();
        }
    }
}