using System.Collections.Generic;
using UnityEngine;

namespace ET.UIFlow
{
    public abstract class UISubBase
    {
    #region 字段
        protected GameObject prefabInstance;
        private bool isShow = false;
    #endregion

    #region 生命周期
        public virtual bool BindComponent()
        {
            return true;
        }

        public void Load(GameObject prefabInstance)
        {
            this.prefabInstance = prefabInstance;
        }
        
        public void Show()
        {
            if (isShow)
                return;
            isShow = true;
            OnShow();
        }

        public void Close()
        {
            isShow = false;
            OnClose();
        }

        public void UnLoad()
        {
            prefabInstance = null;
        }
        
        public void Update()
        {
            if (!isShow)
                return;
            OnUpdate();
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnClose()
        {
        }

        protected virtual void OnUpdate()
        {
        }
    #endregion
    }
}