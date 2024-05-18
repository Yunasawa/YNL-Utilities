#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace YNL.Attributes
{
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public class LabelAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.isEditingMultipleObjects) return 0f;
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.isEditingMultipleObjects) return;

            GUIStyle labelStyle = GUI.skin.label;
            labelStyle.fontStyle = FontStyle.Bold;

            Rect rectPosition = new Rect(position.x, position.y, position.width, position.height + 1);

            EditorGUI.DrawRect(rectPosition, new Color(0.15f, 0.15f, 0.15f, 1));

            Rect labelPosition = new Rect(position.x + 2, position.y + 1, position.width, position.height);
            Rect contentPosition = new Rect(position.x + 125, position.y + 1, position.width, position.height);

            EditorGUI.LabelField(labelPosition, $"{label.text} ▶▶▶", labelStyle);
            GUI.contentColor = new Color(0.976f, 0.878f, 0.463f);
            EditorGUI.LabelField(contentPosition, property.stringValue, labelStyle);
            GUI.contentColor = Color.white;
        }
    }
}
#endif