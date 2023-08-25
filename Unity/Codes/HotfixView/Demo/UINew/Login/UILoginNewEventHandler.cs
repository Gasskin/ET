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
            this.View.LoginBtn.onClick.AddListener((() =>
            {
                var account = this.View.Account.text;
                var password = this.View.Password.text;
                Log.Error($"{account}:{password}");
            }));
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