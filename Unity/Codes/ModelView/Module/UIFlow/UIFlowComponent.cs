using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [ComponentOf(typeof (Scene))]
    [ChildType(typeof (UIFlowWindowComponent))]
    public class UIFlowComponent: Entity, IAwake, IDestroy
    {
        public static UIFlowComponent Instance;
        public Transform UIRoot { get; set; }
        public List<WindowID> WindowListCached { get; } = new();
        public Dictionary<WindowLayer, Transform> LayerDic { get; } = new();
        public Dictionary<WindowID, UIFlowWindowComponent> AllWindowsDic { get; } = new();
        public Dictionary<WindowID, UIFlowWindowComponent> VisibleWindowsDic { get; } = new();
    }
}