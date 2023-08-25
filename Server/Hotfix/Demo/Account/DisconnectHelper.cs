namespace ET
{
    public static class DisconnectHelper
    {
        public static async ETTask Disconnect(this Session self)
        {
            if (self == null || self.IsDisposed)
                return;
            long id = self.InstanceId;
            await TimerComponent.Instance.WaitAsync(3000);
            if (id == self.InstanceId)
            {
                self.Dispose();
            }
        }
    }
}