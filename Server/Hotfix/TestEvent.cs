namespace ET
{
    public class TestEvent: AEventAsync<EventType.TestEvent>
    {
        protected override async ETTask Run(EventType.TestEvent a)
        {
            Log.Debug($"xccc,{TimeHelper.ClientNowSeconds()}");
            await TimerComponent.Instance.WaitAsync(2000);
            Log.Debug($"xccc,{TimeHelper.ClientNowSeconds()}");
        }
    }
}