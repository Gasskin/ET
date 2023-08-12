using System.Collections.Generic;
using ET.UIFlow;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(UIComponentData))]
public class UIComponentDataDrawer : PropertyDrawer
{
    private List<string> components = new List<string>();

    private GUIStyle textField = new GUIStyle("textField");
    private GUIStyle popup = new GUIStyle("popup");

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 1.8f;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var rect = position;
        rect.y += EditorGUIUtility.singleLineHeight * 0.15f;
        rect.height = EditorGUIUtility.singleLineHeight * 1.5f;
        popup.fixedHeight = rect.height;
        textField.alignment = TextAnchor.MiddleCenter;

        var fieldName = property.FindPropertyRelative(nameof(UIComponentData.fieldName));
        var componentType = property.FindPropertyRelative(nameof(UIComponentData.componentType));
        var target = property.FindPropertyRelative(nameof(UIComponentData.target));

        rect.width = position.width * 0.4f;
        var tempTarget = target.objectReferenceValue;
        if (target.objectReferenceValue == null)
            GUI.color = Color.red;
        EditorGUI.ObjectField(rect, target, new GUIContent(""));
        GUI.color = Color.white;
        if (tempTarget != target.objectReferenceValue && target.objectReferenceValue != null)
            fieldName.stringValue = target.objectReferenceValue.name;

        components.Clear();
        var select = 0;
        if (target.objectReferenceValue == null)
        {
            components.Add("NONE");
        }
        else
        {
            var transform = target.objectReferenceValue as Transform;
            var comArr = transform.GetComponents<Component>();
            for (int i = 0; i < comArr.Length; i++)
            {
                var com = comArr[i];
                var name = com.GetType().Name;
                if (componentType.stringValue == name)
                    select = i;
                components.Add(name);
            }
        }

        rect.x += rect.width + 5;
        rect.width = position.width * 0.3f - 5;
        select = EditorGUI.Popup(rect, select, components.ToArray(), popup);
        componentType.stringValue = components[select];

        rect.x += rect.width + 5;
        rect.width = position.width * 0.3f - 5;
        fieldName.stringValue = EditorGUI.TextField(rect, fieldName.stringValue, textField);
    }
}