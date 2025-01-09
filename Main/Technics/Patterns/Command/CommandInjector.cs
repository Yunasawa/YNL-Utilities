using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YNL.Utilities.Extensions;

namespace YNL.Utilities.Patterns
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectCommandAttribute : Attribute { }

    public static class CommandInjector
    {
        public static List<IListenerBase> Commands = new();

        public static void Add(IListenerBase command)
        {
            Commands.Add(command);
        }

        public static void Inject(string assemblyName = "")
        {
            Assembly assembly;
            if (!assemblyName.IsNullOrEmpty()) assembly = Assembly.Load(assemblyName);
            else assembly = Assembly.GetExecutingAssembly();

            var commandTypes = from t in assembly.GetTypes()
                               where t.GetCustomAttributes(typeof(InjectCommandAttribute), false).Length > 0
                               select t;

            foreach (var type in commandTypes)
            {
                var instance = Activator.CreateInstance(type);
                Add((IListenerBase)instance);
            }
        }
    }
}