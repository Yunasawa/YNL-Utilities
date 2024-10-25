using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using System.Linq;
using YNL.Utilities.Addons;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace YNL.Extensions.Methods
{
	public static class MJson
	{
		/// <summary>
		/// Convert data to json string (Not including unserializable type).
		/// </summary>
		public static bool SaveNewtonJson<T>(this T data, string path, Action saveDone = null)
		{
			if (!File.Exists(path))
			{
				File.Create(path);
				Debug.LogWarning("Target json file doesn't exist! Created a new file.");
			}

			string json = JsonConvert.SerializeObject(data, Formatting.Indented);
			File.WriteAllText(path, json);
#if UNITY_EDITOR
			AssetDatabase.Refresh();
#endif
			return true;
		}

		/// <summary>
		/// Load data from json string (Not including unserializable type).
		/// </summary>
		public static T LoadNewtonJson<T>(string path, Action<T> complete = null, Action<string> fail = null)
		{
			T data = JsonConvert.DeserializeObject<T>("");

			if (File.Exists(path))
			{
				string json = File.ReadAllText(path);
				data = JsonConvert.DeserializeObject<T>(json);

				complete?.Invoke(data);
			}
			else
			{
				fail?.Invoke("Load Json Failed!");
			}

			return data;
		}

		/// <summary>
		/// Convert data to json string (Not including unserializable type).
		/// </summary>
		public static bool SaveJson<T>(this T data, string path, Action saveDone = null)
		{
			if (!File.Exists(path))
			{
				File.Create(path);
				Debug.LogWarning("Target json file doesn't exist! Created a new file.");
			}

			string json = JsonUtility.ToJson(data, true);
			File.WriteAllText(path, json);
#if UNITY_EDITOR
			AssetDatabase.Refresh();
#endif
			return true;
		}

		/// <summary>
		/// Load data from json string (Not including unserializable type).
		/// </summary>
		public static T LoadJson<T>(string path, Action<T> complete = null, Action<string> fail = null)
		{
			T data = JsonUtility.FromJson<T>("");

			if (File.Exists(path))
			{
				string json = File.ReadAllText(path);
				data = JsonUtility.FromJson<T>(json);

				complete?.Invoke(data);
			}
			else
			{
				fail?.Invoke("Load Json Failed!");
			}

			return data;
		}
	}

	public static class MAsset
	{
#if UNITY_EDITOR
        public static T[] GetAssets<T>(this string path) where T : UnityEngine.Object
        {
            List<T> assets = new List<T>();
            var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);

            if (asset is DefaultAsset)
            {
                string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { path });
                foreach (var guid in guids)
                {
                    string getPath = AssetDatabase.GUIDToAssetPath(guid);
                    T texture = AssetDatabase.LoadAssetAtPath<T>(getPath);
                    assets.AddDistinct(texture);
                }
            }
            else if (asset is T)
            {
                assets.AddDistinct(asset as T);
            }

            return assets.ToArray();
        }

        public static SerializableDictionary<string, T> GetAssetsDict<T>(this string path) where T : UnityEngine.Object
        {
            T[] assets = path.GetAssets<T>();
            SerializableDictionary<string, T> assetDict = new();

            foreach (var asset in assets) assetDict.Add(asset.name, asset);

            return assetDict;
        }

        public static T LoadAsset<T>(this string path) where T : UnityEngine.Object
			=> AssetDatabase.LoadAssetAtPath<T>(path);
#endif
		public static T LoadResource<T>(this string path) where T : UnityEngine.Object
			=> Resources.Load<T>(path);
	}
}