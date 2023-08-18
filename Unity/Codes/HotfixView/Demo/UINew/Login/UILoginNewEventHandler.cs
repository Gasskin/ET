namespace ET
{
    [UIFlowEvent(WindowID.LoginNew)]
    public class UILoginNewEventHandler: IUIFlowEventHandler
    {
        public void OnLoad()
        {
            Log.Error("OnLoad Window");
        }

        public void OnShow()
        {
        }

        public void OnHide()
        {
        }

        public void OnUnLoad()
        {
        }
    }
}