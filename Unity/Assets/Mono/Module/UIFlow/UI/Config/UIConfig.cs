using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ET.UIFlow
{
    public class UIConfig: ScriptableObject
    {
#if UNITY_EDITOR
        [MenuItem("Assets/Create/UIFlow/UIConfig")]
        public static void CreateConfig()
        {
            var asset = Resources.Load<UIConfig>("UIConfig");

            if (asset == null)
            {
                var select = Selection.activeObject;
                var path = select == null
                        ? "Assets/UIConfig.asset"
                        : $"{AssetDatabase.GetAssetPath(select)}/UIConfig.asset";

                asset = CreateInstance<UIConfig>();
                AssetDatabase.CreateAsset(asset, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            EditorGUIUtility.PingObject(asset);
        }

        public bool enableDebug;
#endif

        [Range(0f, 60f)]
        public float unLoadTime;

        public string genPath;

        public string prefabPath;

        public string[] nameSpaceUse;
    }
}