﻿// <AUTO-GENERATE>
// This File Is Auto Generated By UIComponentEditor
// </AUTO-GENERATE>

using UnityEngine;
using UnityEngine.UI;

namespace ET.UIFlow
{
    public class UILoginNewView
    {
        private UIBindComponent bindComponent;
        public UILoginNewView(GameObject prefab)
        {
            this.bindComponent = prefab.GetComponent<UIBindComponent>();
        }

        private Button _LoginBtn;
        public Button LoginBtn
        {
            get
            {
                if (this._LoginBtn == null && this.bindComponent != null)
                    this._LoginBtn = this.bindComponent.GetBindComponent<Button>(0);
                return this._LoginBtn;
            }
        }

        private InputField _Account;
        public InputField Account
        {
            get
            {
                if (this._Account == null && this.bindComponent != null)
                    this._Account = this.bindComponent.GetBindComponent<InputField>(1);
                return this._Account;
            }
        }

        private InputField _Password;
        public InputField Password
        {
            get
            {
                if (this._Password == null && this.bindComponent != null)
                    this._Password = this.bindComponent.GetBindComponent<InputField>(2);
                return this._Password;
            }
        }

    }
}
