using System.Collections.Generic;
using System;

namespace YNL.Extensions.Methods
{
    public static class MEnum
    {
        #region Custom Attribute EnumFlag Handler Methods
        public static List<int> GetSelectedIndexes<T>(this T val) where T : IConvertible
        {
            List<int> selectedIndexes = new List<int>();
            for (int i = 0; i < System.Enum.GetValues(typeof(T)).Length; i++)
            {
                int layer = 1 << i;
                if ((Convert.ToInt32(val) & layer) != 0)
                {
                    selectedIndexes.Add(i);
                }
            }
            return selectedIndexes;
        }

        public static bool ContainIndex<T>(this List<int> flags, T value)
            => flags.Contains(Convert.ToInt32(value));

        public static List<string> GetSelectedNames<T>(this T val) where T : IConvertible
        {
            List<string> selectedNames = new List<string>();
            for (int i = 0; i < Enum.GetValues(typeof(T)).Length; i++)
            {
                int layer = 1 << i;
                if ((Convert.ToInt32(val) & layer) != 0)
                {
                    selectedNames.Add(Enum.GetValues(typeof(T)).GetValue(i).ToString());
                }
            }
            return selectedNames;
        }

        public static bool ContainFlag<T>(this List<string> flags, T value)
            => flags.Contains(Enum.GetValues(typeof(T)).GetValue(Convert.ToInt32(value)).ToString());
        #endregion


        #region Bit-Enum Handler Methods
        /// <summary>
        /// Use for multiple-choice enum / enum flag to check for equation of enums.
        /// </summary>
        public static bool ContainEnum<T>(this T flag, T value)
            => (Convert.ToInt32(flag) & Convert.ToInt32(value)) != 0;

        /// <summary>
        /// Check if enum flag contains all input flags or not.
        /// </summary>
        public static bool ContainAll<T>(this T flag, params T[] values)
        {
            foreach (var t in values) if ((Convert.ToInt32(flag) & Convert.ToInt32(t)) == 0) return false;
            return true;
        }

        /// <summary>
        /// Return true if enum flag contains just one out of input flags.
        /// </summary>
        public static bool ContainOneOf<T>(this T flag, params T[] values)
        {
            foreach (var t in values) if ((Convert.ToInt32(flag) & Convert.ToInt32(t)) != 0) return true;
            return false;
        }
        #endregion

        /// <summary>
        /// Parse a string into an enum.
        /// </summary>
        public static T Parse<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}