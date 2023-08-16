using System;

namespace ET
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UIFlowEventAttribute: BaseAttribute
    {
        public WindowID WindowID { get; }

        public UIFlowEventAttribute(WindowID id)
        {
            WindowID = id;
        }
    }
}