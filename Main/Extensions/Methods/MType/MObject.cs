using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace YNL.Extensions.Methods
{
    public static class MObject
    {
        /// <summary> 
        /// Check whether object is null or not 
        /// </summary>
        public static bool IsNull(this object obj) => obj == null;
        public static bool IsNullOrDestroyed(this object obj)
        {
            if (object.ReferenceEquals(obj, null)) return true;
            if (obj is UnityEngine.Object) return (obj as UnityEngine.Object) == null;
            return false;
        }

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

        /// <summary>
        /// Detect if this GameObject has the same name with other children in the same path.
        /// </summary>
        public static bool HasDuplicatedNameInSamePath(this GameObject gameObject)
        {
            if (gameObject.transform.parent.IsNull()) return false;
            else return gameObject.transform.parent.Cast<Transform>().Count(i => i.name == gameObject.name) > 1;
        }
    
        /// <summary>
        /// Get full path of a GameObject (contains GO name)
        /// </summary>
        public static string GetPath(this GameObject gameObject, bool isFull = true, string separator = "/")
        {
            string path = "";
            if (isFull) path = gameObject.name;

            while (!gameObject.transform.parent.IsNull())
            {
                gameObject = gameObject.transform.parent.gameObject;
                path = $"{gameObject.name}{separator}{path}";
            }

            if (!isFull && path.Length > 0) path = path.Substring(0, path.Length - separator.Length);

            return path;
        }
        public static string GetPath<T>(this T gameObject, bool isFull = true, string separator = "/") where T : Component
            => gameObject.gameObject.GetPath(isFull, separator);
        public static string GetAnimationPath(this GameObject gameObject, Animator animator, bool isFull = true)
        {
            string path = "";

            if (gameObject.IsNullOrDestroyed()) return path;
            if (isFull) path = gameObject.name;

            bool isRoot = gameObject == animator.gameObject;

            if (isRoot)
            {
                path = "";
            }
            else
            {
                while (gameObject.transform.parent.gameObject != animator.gameObject)
                {
                    gameObject = gameObject.transform.parent.gameObject;
                    path = $"{gameObject.name}/{path}";
                }

                if (!isFull && path.Length > 0) path = path.Substring(0, path.Length - 1);
            }

            return path;
        }

        /// <summary>
        /// Check if an object has parent changed.
        /// </summary>
        public static bool HasParentChanged(this string beforePath, string afterPath)
        {
            string[] beforeComponents = beforePath.Split('/');
            string[] afterComponents = afterPath.Split('/');

            string beforeParent = beforeComponents.Length > 1 ? beforeComponents[^2] : "";
            string afterParent = afterComponents.Length > 1 ? afterComponents[^2] : "";

            return beforeParent != afterParent;
        }
        
        /// <summary>
        /// Check if an object has been renamed.
        /// </summary>
        public static bool HasBeenRenamed(this string beforePath, string afterPath)
        {
            string beforeName = beforePath.Split('/')[^1];
            string afterName = afterPath.Split('/')[^1];
            
            return beforeName != afterName;
        }

        /// <summary>
        /// Find object's parent from a full path.
        /// </summary>
        public static GameObject GetParent(this string fullPath)
        {
            string[] names = fullPath.Split("/");
            if (names.Length <= 1) return null;

            return GameObject.Find(string.Join("/", names.Take(names.Length - 1)));
        }

        /// <summary>
        /// Check if an object is a chidld of another or not.
        /// </summary>
        public static bool IsChildOf(this GameObject gameObject, GameObject parent)
        {
            while (!gameObject.transform.parent.IsNull())
            {
                if (gameObject == parent) return true;
            }

            return false;
        }
    }

    public static class MCoroutine
    {
        public static Coroutine TryStartCoroutine(this MonoBehaviour mono, IEnumerator coroutine)
        {
            if (mono.IsNull() || !mono.gameObject.activeInHierarchy) return null;
            return mono.StartCoroutine(coroutine);
        }

        public static void TryStopCoroutine(this MonoBehaviour mono, Coroutine coroutine)
        {
            if (coroutine.IsNull()) return;
            mono.StopCoroutine(coroutine);
            coroutine = null;
        }

        public static void TryStopCoroutines(this MonoBehaviour mono, List<Coroutine> list)
        {
            foreach (var coroutine in list) mono.TryStopCoroutine(coroutine);
            list.Clear();
        }
    }
}