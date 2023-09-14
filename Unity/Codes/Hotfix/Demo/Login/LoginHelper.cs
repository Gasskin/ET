using System;

namespace ET
{
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene zoneScene, string address, string account, string password)
        {
            A2C_LoginAccount msg = null;
            Session session = null;
            try
            {
                session = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
                password = MD5Helper.StringMD5(password);
                msg = (A2C_LoginAccount)await session.Call(new C2A_LoginAccount() { AccountName = account, Password = password });
            }
            catch (Exception e)
            {
                session?.Dispose();
                Log.Error(e);
                return ErrorCode.ERR_NetWorkError;
            }

            if (msg.Error != ErrorCode.ERR_Success)
            {
                session?.Dispose();
                return msg.Error;
            }

            zoneScene.AddComponent<SessionComponent>().Session = session;
            zoneScene.GetComponent<SessionComponent>().Session.AddComponent<PingComponent>();
            AccountInfoComponent.Instance.SetAccountInfo(msg.Token, msg.AccountId);

            return ErrorCode.ERR_Success;
        }


        public static async ETTask<int> GetServerInfos(Scene zoneScene)
        {
            A2C_GetServerInfos response = null;

            try
            {
                response = (A2C_GetServerInfos) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetServerInfos()
                {
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (response.Error != ErrorCode.ERR_Success)
            {
                return response.Error;
            }

            foreach (var serverInfoProto in response.ServerInfoList)
            {
                var serverInfo = zoneScene.GetComponent<ServerInfosComponent>().AddChild<ServerInfo>();
                serverInfo.FromMessage(serverInfoProto);
                zoneScene.GetComponent<ServerInfosComponent>().Add(serverInfo);
            }
            
            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }
    }
}