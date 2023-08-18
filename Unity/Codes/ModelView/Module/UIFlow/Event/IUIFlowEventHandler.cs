namespace ET
{
    public interface IUIFlowEventHandler
    {
        void OnLoad();
        void OnShow();
        void OnHide();
        void OnUnLoad();
    }
}