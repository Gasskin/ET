using ET.UIFlow;
using UnityEngine;

namespace ET
{
    public class UIFlowWindowComponent: Entity,IAwake,IDestroy
    {
        public WindowID WindowID { get; set; }
        public WindowLayer WindowLayer { get; set; }
        public GameObject Prefab { get; set; }
        public RectTransform Rect { get; set; }
        public CanvasGroup CanvasGroup { get; set; }
    }
}