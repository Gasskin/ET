﻿namespace ET
{
    [ActorMessageHandler]
    public class C2M_TestActorLocationMessageHandler: AMActorLocationHandler<Unit,C2M_TestActorLocationMessage>
    {
        protected override async ETTask Run(Unit unit, C2M_TestActorLocationMessage message)
        {
            Log.Debug(message.Content);
            MessageHelper.SendToClient(unit, new M2C_TestActorMessage() { Content = "M2C_TestActorMessage" });
            await ETTask.CompletedTask;
        }
    }
}