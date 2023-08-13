using System.Collections.Generic;
using UnityEngine;

namespace ET.UIFlow
{
    [UIBase]
    public abstract class UIBase
    {
    #region 字段
        protected GameObject prefabInstance;
        protected RectTransform rectTransform;
        protected CanvasGroup canvasGroup;
        protected Dictionary<int,UISubBase> subUI = new Dictionary<int,UISubBase>();
    #endregion

    #region 属性
        public bool IsOpen => canvasGroup.alpha >= 1;

        public abstract UIType Layer { get; }
        public abstract string PrefabName { get; }
    #endregion

    #region 生命周期
        public virtual bool BindComponent(GameObject instance)
        {
            return true;
        }

        public void Load()
        {
            rectTransform = prefabInstance.transform as RectTransform;
            if (rectTransform != null)
            {
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.anchoredPosition = Vector2.one;
            }

            if (!prefabInstance.TryGetComponent(out canvasGroup))
                canvasGroup = prefabInstance.AddComponent<CanvasGroup>();

            canvasGroup.alpha = 0;
            OnLoad();
        }

        public void Show()
        {
            canvasGroup.alpha = 1;
            OnShow();
            foreach (var sub in subUI.Values)
            {
                sub.Show();
            }
        }

        
        public void Close()
        {
            canvasGroup.alpha = 0;
            foreach (var sub in subUI.Values)
            {
                sub.Close();
            }
            OnClose();
        }

        public void Unload()
        {
            OnUnload();
            subUI.Clear();
            UnityEngine.Object.Destroy(prefabInstance);
            prefabInstance = null;
            canvasGroup = null;
        }

        public void Update()
        {
            OnUpdate();
            foreach (var sub in subUI.Values)
            {
                sub.Update();
            }
        }

        protected virtual void OnLoad()
        {
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnClose()
        {
        }

        protected virtual void OnUnload()
        {
        }

        protected virtual void OnUpdate()
        {
        }
    #endregion

    #region 接口方法
        protected void AddSubUI<T>(RectTransform uiRoot) where T : UISubBase, new()
        {
            AddSubUI<T>(uiRoot.transform);
        }
        
        protected void AddSubUI<T>(GameObject uiRoot) where T : UISubBase, new()
        {
            AddSubUI<T>(uiRoot.transform);
        }
        
        protected void AddSubUI<T>(Transform uiRoot) where T : UISubBase, new()
        {
            var instanceId = uiRoot.GetInstanceID();
            if (HasSubUI(uiRoot))
            {
                Debug.LogError("禁止在某一个节点上多次添加子UI");
                return;
            }

            var uiLogic = new T();
            uiLogic.Load(uiRoot.gameObject);
            if (!uiLogic.BindComponent())
            {
                Debug.LogError($"{typeof(T).Name} 子UI绑定组件失败");
                return;
            }
            subUI.Add(instanceId, uiLogic);
            uiLogic.Show();
        }

        protected void RemoveSubUI<T>(Transform uiRoot) where T : UISubBase
        {
            var instanceId = uiRoot.GetInstanceID();
            if (subUI.TryGetValue(instanceId,out var uiLogic))
            {
                uiLogic.Close();
                subUI.Remove(instanceId);
            }
            else
            {
                Debug.LogError($"不存在子UI {typeof(T).Name}");
            }
        }

        protected bool HasSubUI(Transform uiRoot)
        {
            return subUI.ContainsKey(uiRoot.GetInstanceID());
        }
    #endregion
    }
}