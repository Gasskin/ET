using ET.UIFlow;

namespace ET
{
    public class UIFlowViewComponent: Entity,IAwake<UIBindComponent>
    {
        public UIBindComponent BindComponent { get; set; }
    }
}