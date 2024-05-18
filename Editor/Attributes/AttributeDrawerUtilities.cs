/*
Original: Stulk3
Github: https://github.com/Stulk3/Unity-HideIf-Attribute
*/

#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace YNL.Attributes
{
    public class AttributeDrawerUtilities
    {
        public static object GetTargetObjectOfProperty(SerializedProperty prop)
        {
            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object targetObject = prop.serializedObject.targetObject;
            var elements = path.Split('.');

            // The target object is the object that holds the serialized property, so if there are '.' we
            // stop before the last element of the elements array
            for (int i = 0; i < elements.Length - 1; i++)
            {
                var element = elements[i];
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    targetObject = GetValue_Imp(targetObject, elementName, index);
                }
                else
                {
                    targetObject = GetValue_Imp(targetObject, element);
                }
            }

            return targetObject;
        }

        private static object GetValue_Imp(object source, string name)
        {
            if (source == null) return null;
            var type = source.GetType();

            while (type != null)
            {
                var field = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (field != null) return field.GetValue(source);

                var property = type.GetProperty(name,
                                                BindingFlags.NonPublic | BindingFlags.Public |
                                                BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property != null) return property.GetValue(source, null);
                type = type.BaseType;
            }
            return null;
        }

        private static object GetValue_Imp(object source, string name, int index)
        {
            var enumerable = GetValue_Imp(source, name) as IEnumerable;
            if (enumerable == null) return null;
            var sourceEnum = enumerable.GetEnumerator();

            for (int i = 0; i <= index; i++)
            {
                if (!sourceEnum.MoveNext()) return null;
            }
            return sourceEnum.Current;
        }
    }

    public static class Extensions
    {
        public static Tvalue GetOrAdd<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> takenDictinary, Tkey key, Func<Tvalue> constructor)
        {
            Tvalue value;
            if (takenDictinary.TryGetValue(key, out value)) return value;

            value = constructor();
            takenDictinary[key] = value;
            return value;

        }
    }
}
#endif