using System;

namespace ET
{
    [FriendClass(typeof (RoleInfo))]
    public class C2A_CreateRoleHandler: AMRpcHandler<C2A_CreateRole, A2C_CreateRole>
    {
        protected override async ETTask Run(Session session, C2A_CreateRole request, A2C_CreateRole response, Action reply)
        {
            if (session.DomainScene().SceneType != cfg.Enum.SceneType.Account)
            {
                session.Dispose();
                return;
            }

            var token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);
            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_NetWorkError;
                reply();
                session?.Disconnect().Coroutine();
                return;
            }

            if (session.GetComponent<SessionLockingComponent>() != null) 
            {
                response.Error = ErrorCode.ERR_NetWorkError;
                reply();
                session?.Disconnect().Coroutine();
                return;
            }
            
            if (string.IsNullOrEmpty(request.Name))
            {
                response.Error = ErrorCode.ERR_NetWorkError;
                reply();
                session?.Disconnect().Coroutine();
                return;
            }

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.CreateRole, request.AccountId))
                {
                    var roleInfos = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                            .Query<RoleInfo>(d => d.name == request.Name && d.serverId == request.ServerId);
                    if (roleInfos != null && roleInfos.Count > 0)
                    {
                        response.Error = ErrorCode.ERR_NetWorkError;
                        reply();
                        session?.Disconnect().Coroutine();
                        return;
                    }

                    var roleInfo = session.AddChildWithId<RoleInfo>(IdGenerater.Instance.GenerateUnitId(request.ServerId));
                    roleInfo.name = request.Name;
                    roleInfo.state = (int)RoleInfoState.Normal;
                    roleInfo.serverId = request.ServerId;
                    roleInfo.accountId = request.AccountId;
                    roleInfo.createTime = TimeHelper.ServerNow();
                    roleInfo.lastLoginTime = 0;

                    await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save(roleInfo);

                    response.RoleInfo = roleInfo.ToMessage();
                    reply();
                    roleInfo.Dispose();
                }
            }
        }
    }
}