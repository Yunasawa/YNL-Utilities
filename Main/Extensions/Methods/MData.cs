using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using System.Linq;
using YNL.Utilities.Addons;
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
		public static SerializableDictionary<string, T> GetAssetDict<T>(string path) where T : UnityEngine.Object
		{
			SerializableDictionary<string, T> dictionary = new();
			T[] assets = GetAssetList<T>(path);

			foreach (var asset in assets) dictionary.Add(asset.name, asset);

			return dictionary;
		}

		public static T[] GetAssetList<T>(string path) where T : UnityEngine.Object
		{
			string[] fileEntries = Directory.GetFiles(path);

			return fileEntries.Select(fileName =>
			{
				string assetPath = fileName.Substring(fileName.IndexOf("Assets"));
				assetPath = Path.ChangeExtension(assetPath, null);
				return AssetDatabase.LoadAssetAtPath(assetPath, typeof(T));
			}).OfType<T>().ToArray();
		}

		public static T LoadAsset<T>(this string path) where T : UnityEngine.Object
			=> AssetDatabase.LoadAssetAtPath<T>(path);

		public static T LoadResource<T>(this string path) where T : UnityEngine.Object
			=> Resources.Load<T>(path);
#endif
	}
}