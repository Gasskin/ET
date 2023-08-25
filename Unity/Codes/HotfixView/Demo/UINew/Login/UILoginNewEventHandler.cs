using ET.UIFlow;

namespace ET
{
    [UIFlowEvent(WindowID.LoginNew)]
    public class UILoginNewEventHandler: IUIFlowEventHandler
    {
        private UILoginNewView View { get; set; }
        
        public void OnLoad(UIFlowWindowComponent wnd)
        {
            this.View = new UILoginNewView(wnd.Prefab);
        }

        public void OnShow(Object data)
        {
        }

        public void OnHide()
        {
        }

        public void OnUnLoad()
        {
            this.View.LoginBtn.onClick.RemoveAllListeners();
        }
    }
}