using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;

namespace YNL.Extensions.Methods
{
    public static class MType
    {
        /// <summary>
        /// Check if a type is same or subtype of another.
        /// </summary>
        public static bool IsSameOrSubtype(this Type potentialBase, Type potentialDescendant)
        {
            return potentialDescendant.IsSubclassOf(potentialBase) || potentialDescendant == potentialBase;
        }

        /// <summary>
        /// Check if a type is derived from a generic type.
        /// </summary>
        public static bool IsDerivedFromGenericType(this Type derivedType, Type genericType)
        {
            if (derivedType == null || genericType == null) return false;

            if (!genericType.IsGenericType)
            {
                throw new ArgumentException("The second argument must be a generic type.");
            }

            if (derivedType.IsGenericType && derivedType.GetGenericTypeDefinition() == genericType) return true;

            return IsDerivedFromGenericType(derivedType.BaseType, genericType);
        }

        /// <summary>
        /// Get instances of all inherited type from inputed type. <br></br>
        /// Inherited types should have constructors.
        /// </summary>
        public static IEnumerable<T> GetInheritedTypes<T>(params object[] args)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(asm => asm.GetTypes())
                .Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract)
                .Select(t => (T)Activator.CreateInstance(t));
        }

        /// <summary>
        /// Get fields in a subclass, not including base classes.
        /// </summary>
        public static FieldInfo[] GetFieldsInSubclass<T>() => typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        public static FieldInfo[] GetFieldsInSubclass(this Type type) => type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        /// <summary>
        /// Create an instance of a type by name.
        /// </summary>
        public static object CreateInstance(string typeName, params object[] parameters)
        {
            Type type = Type.GetType(typeName);
            if (!type.IsNull()) return Activator.CreateInstance(type, parameters);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(typeName);
                if (!type.IsNull()) return Activator.CreateInstance(type, parameters);
            }

            MDebug.Error($"'{typeName}' is a type!");
            return null;
        }
    }
}