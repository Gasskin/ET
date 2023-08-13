using System.Collections.Generic;
using UnityEngine;

namespace ET.UIFlow
{
    [ComponentOf(typeof(Scene))]
    public class UIFlowComponent: Entity,IAwake,IDestroy,IUpdate
    {
        public static UIFlowComponent Instance;
        public bool isInit;
        public GameObject uiRoot;
        public Dictionary<UIType, Transform> uiLayer;
        public Dictionary<string, UIBase> uiLogics;
        public Dictionary<string, UIBase> uiWaitForUnLoad;
        public Dictionary<string, float> uiUnLoadCountDown;
        public List<string> removeHelper;
        public UIConfig config;
        public Dictionary<string, UIBaseAttribute> uiInfos;
    }
}