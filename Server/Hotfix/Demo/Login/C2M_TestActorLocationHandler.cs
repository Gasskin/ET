using System;

namespace ET
{
    [ActorMessageHandler]
    public class C2M_TestActorLocationHandler: AMActorLocationRpcHandler<Unit,C2M_TestActorLocationRequest,M2C_TestActorLocationResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_TestActorLocationRequest request, M2C_TestActorLocationResponse response, Action reply)
        {
            Log.Debug(request.Content);
            response.Content = "M2C_TestActorLocationResponse";
            reply();
            await ETTask.CompletedTask;
        }
    }
}