using System;

namespace ET
{
    [ActorMessageHandler]
    public class A2L_LoginAccountHandler: AMActorRpcHandler<Scene, A2L_LoginAccount, L2A_LoginAccount>
    {
        protected override async ETTask Run(Scene unit, A2L_LoginAccount request, L2A_LoginAccount response, Action reply)
        {
            var accountId = request.AccountId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginCenterLock, accountId.GetHashCode()))
            {
                var zone = unit.GetComponent<LoginInfoRecordComponent>().Get(accountId);
                if (zone <= 0)
                {
                    response.Error = ErrorCode.ERR_LoginInfoError;
                    reply();
                    return;
                }

                var gateConfig = RealmGateAddressHelper.GetGate(zone, accountId);
                var g2l_disconnect = (G2L_DissconnectGateUnit) await MessageHelper.CallActor(gateConfig.InstanceID, new L2G_DisconnectGateUnit() { AccountId = accountId });
                response.Error = g2l_disconnect.Error;
                reply();
            }
        }
    }
}