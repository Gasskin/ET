using System;

namespace ET
{
    [FriendClass(typeof (Account))]
    public class C2A_LoginAccountHandler: AMRpcHandler<C2A_LoginAccount, A2C_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2A_LoginAccount request, A2C_LoginAccount response, Action reply)
        {
            if (session.DomainScene().SceneType != cfg.Enum.SceneType.Account)
            {
                Log.Error($"请求的Scene错误，当前的Scene为：{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }

            session.RemoveComponent<SessionAcceptTimeoutComponent>();
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                ReturnErr();
                return;
            }

            if (string.IsNullOrEmpty(request.AccountName) || string.IsNullOrEmpty(request.Password))
            {
                ReturnErr();
                return;
            }

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount, request.AccountName.Trim().GetHashCode()))
                {
                    var accountInfoList = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                            .Query<Account>((account => account.accountName.Equals(request.AccountName.Trim())));
                    Account account = null;
                    if (accountInfoList.Count > 0)
                    {
                        account = accountInfoList[0];
                        session.AddChild(account);
                        if (account.accountType == (int)AccountType.BlackList)
                        {
                            ReturnErr();
                            account.Dispose();
                            return;
                        }

                        if (!account.password.Equals(request.Password))
                        {
                            ReturnErr();
                            account.Dispose();
                            return;
                        }
                    }
                    else
                    {
                        account = session.AddChild<Account>();
                        account.accountName = request.AccountName.Trim();
                        account.password = request.Password;
                        account.createTime = TimeHelper.ServerNow();
                        account.accountType = (int)AccountType.General;
                        await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save(account);
                    }

                    // 如果这个账号已经登陆过了，那么踢掉
                    var sessionId = session.DomainScene().GetComponent<AccountSessionsComponent>().Get(account.Id);
                    var sessionExit = Game.EventSystem.Get(sessionId) as Session;
                    sessionExit?.Send(new A2C_Disconnect() { Error = 0 });
                    sessionExit?.Disconnect();
                    session.DomainScene().GetComponent<AccountSessionsComponent>().Add(account.Id, session.InstanceId);
                    session.AddComponent<AccountCheckOutTimeComponent, long>(account.Id);
                    
                    var token = TimeHelper.ServerNow().ToString() + RandomHelper.RandomNumber(int.MinValue, int.MaxValue);
                    session.DomainScene().GetComponent<TokenComponent>().Remove(account.Id);
                    session.DomainScene().GetComponent<TokenComponent>().Add(account.Id, token);

                    response.AccountId = account.Id;
                    response.Token = token;
                    reply();
                    account.Dispose();
                }
            }
            
            void ReturnErr()
            {
                response.Error = ErrorCode.ERR_LoginInfoError;
                reply();
                session.Disconnect().Coroutine();
            }
        }
    }
}