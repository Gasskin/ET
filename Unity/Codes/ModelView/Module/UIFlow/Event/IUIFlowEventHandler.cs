namespace ET
{
    public interface IUIFlowEventHandler
    {
        void OnLoad(UIFlowWindowComponent wnd);
        void OnShow(Object data);
        void OnHide();
        void OnUnLoad();
    }
}