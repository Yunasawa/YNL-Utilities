using UnityEngine;

namespace YNL.Extensions.Methods
{
    public static class MDebug
    {
        public static void Log(object message)
            => Debug.Log($"<color=#9EFFF9><b>ⓘ Log:</b></color> {message}");

        public static void Warning(object message)
            => Debug.LogWarning($"<color=#FFE045><b>⚠ Warning:</b></color> {message}");

        public static void Caution(object message)
            => Debug.Log($"<color=#FF983D><b>⚠ Caution:</b></color> {message}");

        public static void Action(object message)
            => Debug.Log($"<color=#EC82FF><b>▶ Action:</b></color> {message}");

        public static void Notify(object message)
            => Debug.Log($"<color=#FFCD45><b>▶ Notification:</b></color> {message}");

        public static void Error(object message)
            => Debug.LogError($"<color=#FF3C2E><b>⚠ Error:</b></color> {message}");

        public static void Custom(string custom = "Custom", object message = null, string color = "#63ff9a")
            => Debug.Log($"<color={color}><b>✔ {custom}:</b></color> {message}");
    }
}