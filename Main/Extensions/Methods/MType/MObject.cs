using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace YNL.Extensions.Methods
{
    public static class MObject
    {
        /// <summary> 
        /// Check whether object is null or not 
        /// </summary>
        public static bool IsNull(this object obj)
            => obj == null || ReferenceEquals(obj, null) || obj.Equals(null);

        /// <summary>
        /// Destroy an object/component/asset in OnValidate(), while Destroy() and DestroyImmediate() are not working.
        /// </summary>
        public static void DestroyOnValidate(this UnityEngine.Object component)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.delayCall += () => MonoBehaviour.DestroyImmediate(component);
#endif
        }

        /// <summary>
        /// Create new GameObject.
        /// </summary>
        public static GameObject CreateGameObject(this Transform parent, params Type[] components)
            => parent.CreateGameObject("GameObject", Vector3.zero, Quaternion.identity, components);
        public static GameObject CreateGameObject(this Transform parent, string name, params Type[] components)
            => parent.CreateGameObject(name, Vector3.zero, Quaternion.identity, components);
        public static GameObject CreateGameObject(params Type[] components)
            => CreateGameObject(null, "Game Object", Vector3.zero, Quaternion.identity, components);
        public static GameObject CreateGameObject(string name, params Type[] components)
            => CreateGameObject(null, name, Vector3.zero, Quaternion.identity, components);
        public static GameObject CreateGameObject(this Transform parent, string name, Vector3 position, Quaternion rotation, params Type[] components)
        {
            GameObject gameObject = new GameObject(name);
            gameObject.transform.parent = parent;
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;

            foreach (Type component in components)
            {
                //Type type = component.GetType();
                gameObject.AddComponent(component);
            }

            return gameObject;
        }

        /// <summary>
        /// Set active a GameObject after "time".
        /// </summary>
        public static async void SetActive(this GameObject gameObject, bool active, float time)
        {
            await Task.Delay((int)(time * 1000));
            gameObject.SetActive(active);
        }
    }

    public static class MCoroutine
    {
        /// <summary>
        /// Start a coroutine.
        /// </summary>
        public static Coroutine StartACoroutine(this MonoBehaviour mono, IEnumerator enumerator)
            => mono.StartCoroutine(enumerator);

        /// <summary>
        /// Stop a coroutine.
        /// </summary>
        public static void StopACoroutine(this MonoBehaviour mono, Coroutine coroutine)
        {
            if (coroutine.IsNull()) return;
            mono.StopCoroutine(coroutine);
            coroutine = null;
        }

        /// <summary>
        /// Stop a list of Coroutines.
        /// </summary>
        public static void StopCoroutines(this MonoBehaviour mono, List<Coroutine> list)
        {
            foreach (var coroutine in list) mono.StopACoroutine(coroutine);
            list.Clear();
        }
    }
}