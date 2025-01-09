using System.Collections.Generic;

namespace YNL.Utilities.Extensions
{
    public static class MDictionary
    {
        /// <summary>
        /// Only use for Dictionary with value is bool. Will return false if dictionary doesn't contain given key.
        /// </summary>
        public static bool GetBool<K>(this Dictionary<K, bool> dictionary, K key)
        {
            if (!dictionary.ContainsKey(key)) return false;
            else return dictionary[key];
        }

        /// <summary>
        /// Get a key by a value in dictionary.
        /// </summary>
        public static K GetKeyByValue<K, V>(this Dictionary<K, V> dictionary, V value)
        {
            foreach (var pair in dictionary)
            {
                if (pair.Value.Equals(value)) return pair.Key;
            }
            return default;
        }
    }
}