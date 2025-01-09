using System;
using UnityEditor;
using UnityEngine;

namespace YNL.Utilities.Addons
{
    [System.Serializable]
    public struct MRange : IEquatable<MRange>
    {
        public float Min;
        public float Max;

        public float Average => (Min + Max) / 2;
        public float Distance => Max - Min;

        public MRange(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public bool InRange(float number, bool equalLeft = true, bool equalRight = true)
        {
            if (equalLeft && !equalRight) return number >= Min && number < Max;
            if (!equalLeft && equalRight) return number > Min && number <= Max;
            if (equalLeft && equalRight) return number >= Min && number <= Max;
            return number > Min && number < Max;
        }

        public bool InOpenInterval(float number) => InRange(number, false, false); // (a, b)
        public bool InCloseInterval(float number) => InRange(number, true, true); // [a, b]
        public bool InLeftCloseInterval(float number) => InRange(number, true, false); // [a, b)
        public bool InRightCloseInterval(float number) => InRange(number, false, true); // (a, b]

        public override string ToString() => $"Min: {Min} | Max: {Max} | Average: {Average} | Distance: {Distance}";
        public override bool Equals(object obj) => base.Equals(obj);
        public bool Equals(MRange range) => this == range;
        public override int GetHashCode() => base.GetHashCode();

        public static MRange operator +(MRange a) => new MRange(+a.Min, +a.Max);
        public static MRange operator +(MRange a, MRange b) => new MRange(a.Min + b.Min, a.Max + b.Max);
        public static MRange operator -(MRange a) => new MRange(-a.Min, -a.Max);
        public static MRange operator -(MRange a, MRange b) => new MRange(a.Min - b.Min, a.Max - b.Max);
        public static MRange operator *(MRange a, MRange b) => new MRange(a.Min * b.Min, a.Max * b.Max);
        public static MRange operator /(MRange a, MRange b) => new MRange(a.Min / b.Min, a.Max / b.Max);
        public static MRange operator %(MRange a, MRange b) => new MRange(a.Min % b.Min, a.Max % b.Max);
        public static bool operator ==(MRange a, MRange b) => a.Min == b.Min && a.Max == b.Max ? true : false;
        public static bool operator !=(MRange a, MRange b) => a.Min != b.Min || a.Max != b.Max ? true : false;
        public static bool operator <(MRange a, MRange b) => a.Distance < b.Distance;
        public static bool operator <=(MRange a, MRange b) => a.Distance <= b.Distance;
        public static bool operator >(MRange a, MRange b) => a.Distance > b.Distance;
        public static bool operator >=(MRange a, MRange b) => a.Distance >= b.Distance;
    }

#if UNITY_EDITOR
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
#endif
}
