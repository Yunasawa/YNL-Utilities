#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace YNL.Extensions.Addons
{
    [CustomPropertyDrawer(typeof(MRange))]
    public class MRangeDrawer : PropertyDrawer
    {
        int _labelWidth = 35;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.isEditingMultipleObjects) return 0f;
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUIStyle style = GUI.skin.box;
            style.fontStyle = FontStyle.Bold;

            if (property.serializedObject.isEditingMultipleObjects) return;

            var minProperty = property.FindPropertyRelative("Min");
            var maxProperty = property.FindPropertyRelative("Max");
            var typeProperty = property.FindPropertyRelative("Type");

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var minLabelRect = new Rect(position.x, position.y, _labelWidth, position.height);
            var minRect = new Rect(position.x + _labelWidth, position.y, position.width / 2 - _labelWidth - 5, position.height);
            var maxLabelRect = new Rect(position.x + position.width / 2 + 5, position.y, _labelWidth, position.height);
            var maxRect = new Rect(position.x + position.width / 2 + _labelWidth + 5, position.y, position.width / 2 - _labelWidth - 5, position.height);

            GUI.contentColor = new Color(0.682f, 0.953f, 0.349f);
            GUI.backgroundColor = new Color(0.5f, 0.5f, 0.5f, 1);
            GUI.Button(minLabelRect, "Min:", style);

            GUI.backgroundColor = Color.white;
            GUI.contentColor = Color.white;
            minProperty.floatValue = EditorGUI.FloatField(minRect, minProperty.floatValue);

            GUI.contentColor = new Color(0.941f, 0.502f, 0.502f);
            GUI.backgroundColor = new Color(0.5f, 0.5f, 0.5f, 1);
            GUI.Button(maxLabelRect, "Max:", style);

            GUI.backgroundColor = Color.white;
            GUI.contentColor = Color.white;
            maxProperty.floatValue = EditorGUI.FloatField(maxRect, maxProperty.floatValue);
        }
    }
}
#endif
