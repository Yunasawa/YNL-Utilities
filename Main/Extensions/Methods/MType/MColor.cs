using System;
using UnityEngine;

namespace YNL.Utilities.Extensions
{
    public static class MColor
    {
        /// <summary>
        /// Normalize the RGB values of Color from range of 0 - 255 into range of 0 - 1
        /// </summary>
        public static Color Normalize(this Color color)
            => new Color(color.r / 255, color.g / 255, color.b / 255, color.a);

        /// <summary>
        /// Lighten a color if <i>factor</i> is greater then 1, darken if <i>factor</i> is smaller than 1.
        /// </summary>
        public static Color Adjust(this Color color, float factor)
        {
            return new Color(
            Mathf.Clamp01(color.r * factor),
            Mathf.Clamp01(color.g * factor),
            Mathf.Clamp01(color.b * factor),
            color.a);
        }

        /// <summary>
        /// Convert from RGBA Color into HEX Color.
        /// </summary>
        public static string ToHex(this Color color)
            => $"#{(int)(color.r * 255):X2}{(int)(color.g * 255):X2}{(int)(color.b * 255):X2}";

        /// <summary>
        /// Convert from HEX Color into RGBA Color.
        /// </summary>
        public static Color ToColor(this string hex)
        {
            Color color = Color.white;

            string newHex = hex;

            if (hex.Contains("#")) newHex = hex.Replace("#", "");

            try
            {
                if (newHex.Length != 6 && newHex.Length != 8)
                {
                    Debug.Log("Format Exception: Invalid HEX Color Format!");
                    return color;
                }
                else if (newHex.Length == 6)
                {
                    color.r = newHex.Substring(0, 2).HexToInt() / (float)255;
                    color.g = newHex.Substring(2, 2).HexToInt() / (float)255;
                    color.b = newHex.Substring(4, 2).HexToInt() / (float)255;
                }
                else if (newHex.Length == 8)
                {
                    color.r = newHex.Substring(0, 2).HexToInt() / (float)255;
                    color.g = newHex.Substring(2, 2).HexToInt() / (float)255;
                    color.b = newHex.Substring(4, 2).HexToInt() / (float)255;
                    color.a = newHex.Substring(6, 2).HexToInt() / (float)255;
                }
            }
            catch (FormatException)
            {
                Debug.Log("Format Exception: Invalid HEX Format.");
            }
            return color;
        }

        /// <summary>
        /// 
        /// </summary>
        public static Color RandomColor(int min, int max, float alpha = 1)
        {
            int r = UnityEngine.Random.Range(min, max);
            int g = UnityEngine.Random.Range(min, max);
            int b = UnityEngine.Random.Range(min, max);
            return new Color(r, g, b, alpha).Normalize();
        }
        
    }
}