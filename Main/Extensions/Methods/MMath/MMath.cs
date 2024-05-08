using UnityEngine;

namespace YNL.Extension.Method
{
    public static class MMath
    {
        /// <summary> 
        /// Returns the value with FPS's change 
        /// </summary>
        public static float Oscillate(this float value, float scale = 60) => (float)value * scale * Time.deltaTime;
    }
}