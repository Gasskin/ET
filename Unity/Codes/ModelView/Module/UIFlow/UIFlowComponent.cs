using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof (Scene))]
    public class UIFlowComponent: Entity, IAwake, IDestroy
    {
        public Dictionary<int, UIFlowWindowComponent> AllWindowsDic { get; set; } = new();
        public List<WindowID> WindowListCached { get; set; } = new();
        public Dictionary<int, UIFlowWindowComponent> VisibleWindowsDic { get; set; } = new();
    }
}