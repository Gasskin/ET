using UnityEngine;

namespace ET
{
    public class UIFlowWindowComponent: Entity,IAwake,IDestroy
    {
        public WindowID WindowID { get; set; }
        public GameObject Prefab { get; set; }
    }
}