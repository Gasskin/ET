using System;

namespace ET
{
    public class UIBaseAttribute: BaseAttribute
    {
        public string UIType { get; }

        public UIBaseAttribute(string uiType)
        {
            this.UIType = uiType;
        }
    }
}