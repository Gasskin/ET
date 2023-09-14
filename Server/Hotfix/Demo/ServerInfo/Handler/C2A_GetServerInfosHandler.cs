using System;

namespace ET
{
    public class C2A_GetServerInfosHandler: AMRpcHandler<C2A_GetServerInfos,A2C_GetServerInfos>
    {
        protected override async ETTask Run(Session session, C2A_GetServerInfos request, A2C_GetServerInfos response, Action reply)
        {
            if (session.DomainScene().SceneType != cfg.Enum.SceneType.Account)
            {
                Log.Error($"请求Scene错误，当前Scene为：{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }

            var token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);
            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_NetWorkError;
                reply();
                session.Disconnect().Coroutine();
            }

            foreach (var serverInfo in session.DomainScene().GetComponent<ServerInfoComponent>().GetAllServerInfo())
            {
                response.ServerInfoList.Add(serverInfo.ToMessage());
            }

            reply();
            
            await ETTask.CompletedTask;
        }
    }
}