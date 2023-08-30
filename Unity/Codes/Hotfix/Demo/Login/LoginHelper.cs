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

        public static async ETTask LoginTest(Scene zoneScene, string address)
        {
            try
            {
                Session session = null;
                R2C_LoginTest r2CLoginTest = null;
                try
                {
                    session = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
                    r2CLoginTest = (R2C_LoginTest)await session.Call(new C2R_LoginTest() { Account = "", Password = "" });
                    Log.Debug(r2CLoginTest.Key);
                    session.Send(new C2R_SayHello() { Hello = "Hello" });
                    await TimerComponent.Instance.WaitAsync(2000);
                }
                finally
                {
                    session?.Dispose();
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
}