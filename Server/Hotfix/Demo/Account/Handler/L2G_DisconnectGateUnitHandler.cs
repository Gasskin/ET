using System;

namespace ET
{
    [ActorMessageHandler]
    public class L2G_DisconnectGateUnitHandler: AMActorRpcHandler<Scene,L2G_DisconnectGateUnit,G2L_DissconnectGateUnit>
    {
        protected override async ETTask Run(Scene unit, L2G_DisconnectGateUnit request, G2L_DissconnectGateUnit response, Action reply)
        {
            var accountId = request.AccountId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GateLoginLock, accountId.GetHashCode()))
            {
                var player = unit.GetComponent<PlayerComponent>();
                var gate = player.Get(accountId);
                if (gate == null)
                {
                    reply();
                    return;
                }
                gate?.Dispose();
                reply();
            }
            await ETTask.CompletedTask;
        }
    }
}