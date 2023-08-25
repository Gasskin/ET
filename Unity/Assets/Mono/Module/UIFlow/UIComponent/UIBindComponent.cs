using UnityEngine;

namespace ET.UIFlow
{
    public class UIBindComponent : MonoBehaviour
    {
        public string uiName;

        public UIComponentData[] uiData;
        
        public T GetBindComponent<T>(int index) where T : Component
        {
            if (index >= uiData.Length) 
            {
                Log.Error("UIComponent 获取组件 溢出");
                return null;
            }

            var data = uiData[index];
            return data.target.GetComponent(data.componentType) as T;
        }
    }
}