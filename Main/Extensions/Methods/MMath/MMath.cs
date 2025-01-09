using System;
using UnityEngine;
using YNL.Utilities.Addons;

namespace YNL.Utilities.Extensions
{
    public static class MMath
    {
        public static bool IsEven(this int number) => number % 2 == 0;
        public static bool IsOdd(this int number) => number % 2 != 0;

        /// <summary>
        /// Convert a numeric string into decimal number.
        /// </summary>
        public static int ToInt(this string input)
        {
            try
            {
                return input.IsNullOrEmpty() ? 0 : int.Parse(input);
            }
            catch (FormatException)
            {
                return 0;
            }
        }

        /// <summary>
        /// Convert Hex into Integer.
        /// </summary>
        public static int HexToInt(this string hex)
        {
            int output = 0;
            try
            {
                output = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Format Exception: Invalid HEX Format.");
            }
            return output;
        }

        /// <summary> 
        /// Returns the value with FPS's change 
        /// </summary>
        public static float Oscillate(this float value, float scale = 60) => (float)value * scale * Time.deltaTime;

        /// <summary>
        /// Remap a value from an original range into a target range.
        /// </summary>
        public static float Remap(this float value, MRange origin, MRange target)
        {
            float tempvalue = Math.Max(origin.Min, Math.Min(origin.Max, value));

            float normalizedValue = (tempvalue - origin.Min) / origin.Distance;
            float remappedValue = target.Min + (normalizedValue * target.Distance);

            return remappedValue;
        }
        public static float RefRemap(this ref float value, MRange origin, MRange target)
        {
            value = Math.Max(origin.Min, Math.Min(origin.Max, value));

            float normalizedValue = (value - origin.Min) / origin.Distance;
            float remappedValue = target.Min + (normalizedValue * target.Distance);

            return remappedValue;
        }

        /// <summary>
        /// Check if a value is in range of a <see cref="MRange"/> <br></br>
        /// </summary>
        public static bool InRange(this float value, MRange range) => range.InRange(value);

        /// <summary>
        /// Percentization a <b><i>Value</i></b> to scale of <b><i>Range</i></b>. 
        /// </summary>
        public static float Percent(this float value, float range) => value / range;
        public static float Percent(this float value) => value / 100;

        /// <summary> 
        /// Round float into custom amount of digit 
        /// </summary>
        public static float RoundToDigit(this float input, int digit) => (float)Math.Round(input, digit);

        /// <summary>
        /// Lock a value in range of min and max.
        /// </summary>
        public static float RefLimit(this ref float value, float min, float max)
        {
            if (value < min) value = min;
            else if (value > max) value = max;
            return value;
        }
        public static int RefLimit(this ref int value, int min, int max)
        {
            if (value < min) value = min;
            else if (value > max) value = max;
            return value;
        }
        public static float Limit(this float value, float min, float max)
        {
            if (value < min) value = min;
            else if (value > max) value = max;
            return value;
        }
        public static int Limit(this int value, int min, int max)
        {
            if (value < min) value = min;
            else if (value > max) value = max;
            return value;
        }

        /// <summary>
        /// Limit a value to smaller value.
        /// </summary>
        public static float LimitMin(this float value, float min)
        {
            if (value < min) value = min;
            return value;
        }
        public static int LimitMin(this int value, int min)
        {
            if (value < min) value = min;
            return value;
        }

        /// <summary>
        /// Limit a value to smaller value.
        /// </summary>
        public static float LimitMax(this float value, float max)
        {
            if (value > max) value = max;
            return value;
        }
        public static int LimitMax(this int value, int max)
        {
            if (value > max) value = max;
            return value;
        }
    }
}