#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace YNL.Attributes
{
    public abstract class ShowingAttributeDrawer : PropertyDrawer
    {

        public static bool CheckShouldHide(SerializedProperty property)
        {
            try
            {
                bool shouldShow = false;

                var targetObject = AttributeDrawerUtilities.GetTargetObjectOfProperty(property);
                var type = targetObject.GetType();

                FieldInfo field;
                do
                {
                    field = type.GetField(property.name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    type = type.BaseType;
                }
                while (field == null && type != null);

                var customAttributes = field.GetCustomAttributes(typeof(ShowingAttribute), false);

                SerializedProperty propertyParent = null;
                var propertyPath = property.propertyPath;
                var lastDot = propertyPath.LastIndexOf('.');
                if (lastDot > 0)
                {
                    var parentPath = propertyPath.Substring(0, lastDot);
                    propertyParent = property.serializedObject.FindProperty(parentPath);
                }

                ShowingAttribute[] attachedAttributes = (ShowingAttribute[])customAttributes;
                foreach (var shower in attachedAttributes)
                {
                    if (!ShouldDraw(property.serializedObject, propertyParent, shower))
                    {
                        shouldShow = true;
                    }
                }

                return shouldShow;
            }
            catch
            {
                return false;
            }
        }


        private static Dictionary<Type, Type> typeToDrawerType;

        private static Dictionary<Type, PropertyDrawer> drawerTypeToDrawerInstance;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!CheckShouldHide(property))
            {
                if (typeToDrawerType == null)
                    PopulateTypeToDrawer();

                Type drawerType;
                var typeOfProp = AttributeDrawerUtilities.GetTargetObjectOfProperty(property).GetType();
                if (typeToDrawerType.TryGetValue(typeOfProp, out drawerType))
                {
                    var drawer = drawerTypeToDrawerInstance.GetOrAdd(drawerType, () => CreateDrawerInstance(drawerType));
                    drawer.OnGUI(position, property, label);
                }
                else
                {
                    EditorGUI.PropertyField(position, property, label, true);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (CheckShouldHide(property))
                return -2;

            if (typeToDrawerType == null)
                PopulateTypeToDrawer();

            Type drawerType;
            var typeOfProp = AttributeDrawerUtilities.GetTargetObjectOfProperty(property).GetType();
            if (typeToDrawerType.TryGetValue(typeOfProp, out drawerType))
            {
                var drawer = drawerTypeToDrawerInstance.GetOrAdd(drawerType, () => CreateDrawerInstance(drawerType));
                return drawer.GetPropertyHeight(property, label);
            }
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        private PropertyDrawer CreateDrawerInstance(Type drawerType)
        {
            return (PropertyDrawer)Activator.CreateInstance(drawerType);
        }

        private void PopulateTypeToDrawer()
        {
            typeToDrawerType = new Dictionary<Type, Type>();
            drawerTypeToDrawerInstance = new Dictionary<Type, PropertyDrawer>();
            var propertyDrawerType = typeof(PropertyDrawer);
            var targetType = typeof(CustomPropertyDrawer).GetField("m_Type", BindingFlags.Instance | BindingFlags.NonPublic);
            var useForChildren = typeof(CustomPropertyDrawer).GetField("m_UseForChildren", BindingFlags.Instance | BindingFlags.NonPublic);

            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());

            foreach (Type type in types)
            {
                if (propertyDrawerType.IsAssignableFrom(type))
                {
                    var customPropertyDrawers = type.GetCustomAttributes(true).OfType<CustomPropertyDrawer>().ToList();
                    foreach (var propertyDrawer in customPropertyDrawers)
                    {
                        var targetedType = (Type)targetType.GetValue(propertyDrawer);
                        typeToDrawerType[targetedType] = type;

                        var usingForChildren = (bool)useForChildren.GetValue(propertyDrawer);
                        if (usingForChildren)
                        {
                            var childTypes = types.Where(t => targetedType.IsAssignableFrom(t) && t != targetedType);
                            foreach (var childType in childTypes)
                            {
                                typeToDrawerType[childType] = type;
                            }
                        }
                    }

                }
            }
        }

        private static bool ShouldDraw(SerializedObject hidingobject, SerializedProperty serializedProperty, ShowingAttribute shower)
        {
            var showIf = shower as ShowIfBoolAttribute;
            if (showIf != null)
            {
                return ShowIfBoolAttributeDrawer.ShouldDraw(hidingobject, serializedProperty, showIf);
            }

            var showIfNull = shower as ShowIfNullAttribute;
            if (showIfNull != null)
            {
                return ShowIfNullAttributeDrawer.ShouldDraw(hidingobject, serializedProperty, showIfNull);
            }

            var showIfEnum = shower as ShowIfEnumAttribute;
            if (showIfEnum != null)
            {
                return ShowIfEnumAttributeDrawer.ShouldDraw(hidingobject, serializedProperty, showIfEnum);
            }

            var showIfCompare = shower as ShowIfValueAttribute;
            if (showIfCompare != null)
            {
                return ShowIfValueAttributeDrawer.ShouldDraw(hidingobject, serializedProperty, showIfCompare);
            }

            Debug.LogWarning("Trying to check unknown shower loadingType: " + shower.GetType().Name);
            return false;
        }

    }

    [CustomPropertyDrawer(typeof(ShowIfBoolAttribute))]
    public class ShowIfBoolAttributeDrawer : ShowingAttributeDrawer
    {
        public static bool ShouldDraw(SerializedObject ShowingObject, SerializedProperty serializedProperty, ShowIfBoolAttribute attribute)
        {
            var prop = serializedProperty == null ? ShowingObject.FindProperty(attribute.variable) : serializedProperty.FindPropertyRelative(attribute.variable);
            if (prop == null)
            {
                return true;
            }
            return prop.boolValue == attribute.state;
        }
    }

    [CustomPropertyDrawer(typeof(ShowIfNullAttribute))]
    public class ShowIfNullAttributeDrawer : ShowingAttributeDrawer
    {
        public static bool ShouldDraw(SerializedObject ShowingObject, SerializedProperty serializedProperty, ShowIfNullAttribute attribute)
        {
            var prop = serializedProperty == null ? ShowingObject.FindProperty(attribute.variable) : serializedProperty.FindPropertyRelative(attribute.variable);
            if (prop == null)
            {
                return true;
            }

            return attribute.isNull ? prop.objectReferenceValue == null : prop.objectReferenceValue != null;
        }
    }

    [CustomPropertyDrawer(typeof(ShowIfEnumAttribute))]
    public class ShowIfEnumAttributeDrawer : ShowingAttributeDrawer
    {
        public static bool ShouldDraw(SerializedObject ShowingObject, SerializedProperty serializedProperty, ShowIfEnumAttribute ShowIfEnumValueAttribute)
        {
            var enumProp = serializedProperty == null ? ShowingObject.FindProperty(ShowIfEnumValueAttribute.variable) : serializedProperty.FindPropertyRelative(ShowIfEnumValueAttribute.variable);
            var state = ShowIfEnumValueAttribute.state;

            //enumProp.enumValueIndex gives the order in the enum list, not the actual enum value
            bool equal = state == enumProp.intValue;

            return equal == ShowIfEnumValueAttribute.showIfEqual;
        }
    }

    [CustomPropertyDrawer(typeof(ShowIfValueAttribute))]
    public class ShowIfValueAttributeDrawer : ShowingAttributeDrawer
    {
        public static bool ShouldDraw(SerializedObject ShowingObject, SerializedProperty serializedProperty, ShowIfValueAttribute ShowIfCompareValueAttribute)
        {
            var variable = serializedProperty == null ? ShowingObject.FindProperty(ShowIfCompareValueAttribute.variable) : serializedProperty.FindPropertyRelative(ShowIfCompareValueAttribute.variable);
            var compareValue = ShowIfCompareValueAttribute.value;

            switch (ShowIfCompareValueAttribute.showIf)
            {
                case ShowIf.Equal: return variable.intValue == compareValue;
                case ShowIf.NotEqual: return variable.intValue != compareValue;
                case ShowIf.Greater: return variable.intValue >= compareValue;
                default: /*case ShowIf.Lower:*/ return variable.intValue <= compareValue;
            }
        }
    }
}
#endif