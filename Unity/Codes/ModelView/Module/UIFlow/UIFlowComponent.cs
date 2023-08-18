using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof (Scene))]
    [ChildType(typeof(UIFlowWindowComponent))]
    public class UIFlowComponent: Entity, IAwake, IDestroy
    {
        public Dictionary<WindowID, UIFlowWindowComponent> AllWindowsDic { get; set; } = new();
        public List<WindowID> WindowListCached { get; set; } = new();
        public Dictionary<WindowID, UIFlowWindowComponent> VisibleWindowsDic { get; set; } = new();
    }
}