/*
Original: Stulk3
Github: https://github.com/Stulk3/Unity-HideIf-Attribute
*/

using System;
using UnityEngine;

namespace YNL.Attributes
{
    public abstract class ShowingAttribute : PropertyAttribute { }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class ShowIfBoolAttribute : ShowingAttribute
    {
        public readonly string variable;
        public readonly bool state;

        public ShowIfBoolAttribute(string variable, bool state, int order = 0)
        {
            this.variable = variable;
            this.state = state;
            this.order = order;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class ShowIfNullAttribute : ShowingAttribute
    {
        public readonly string variable;
        public readonly bool isNull;

        public ShowIfNullAttribute(string variable, bool isNull = true, int order = 0)
        {
            this.variable = variable;
            this.isNull = isNull;
            this.order = order;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class ShowIfEnumAttribute : ShowingAttribute
    {
        public readonly string variable;
        public readonly bool showIfEqual;
        public readonly int state;

        public ShowIfEnumAttribute(string variable, int state, ShowIf showIf = ShowIf.Equal)
        {
            this.variable = variable;
            this.showIfEqual = showIf == ShowIf.Equal;
            this.state = state;
            this.order = -1;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class ShowIfValueAttribute : ShowingAttribute
    {
        public readonly string variable;
        public readonly ShowIf showIf;
        public readonly int value;

        public ShowIfValueAttribute(string variable, int value, ShowIf showIf = ShowIf.Equal)
        {
            this.variable = variable;
            this.showIf = showIf;
            this.value = value;
            this.order = -1;
        }
    }

    public enum ShowIf
    {
        Equal,
        NotEqual,
        Greater,
        Lower
    }
}