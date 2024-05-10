using UnityEngine;
using System.Collections.Generic;

namespace YNL.Utilities.Addons
{
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> _keys = new List<TKey>();
        [SerializeField] private List<TValue> _values = new List<TValue>();

        public SerializableDictionary() { }

        /// <summary> Save the dictionary to lists </summary>
        public void OnBeforeSerialize()
        {
            _keys.Clear();
            _values.Clear();
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                _keys.Add(pair.Key);
                _values.Add(pair.Value);
            }
        }

        /// <summary> Load the dictionary from lists </summary>
        public void OnAfterDeserialize()
        {
            this.Clear();

            if (_keys.Count != _values.Count)
            {
                throw new System.Exception(string.Format("There are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));
            }

            for (int i = 0; i < _keys.Count; i++)
            {
                this.Add(_keys[i], _values[i]);
            }
        }
    }
}