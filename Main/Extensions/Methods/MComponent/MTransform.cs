using System.Linq;
using UnityEngine;

namespace YNL.Extension.Method
{
    public static class MTransform
    {
        /// <summary> 
        /// Rotates the current transform toward the target transform just by Y axis 
        /// </summary>
        public static void LookAtByY(this Transform currentTransform, Transform targetTransform)
        {
            currentTransform.LookAt(new Vector3(targetTransform.position.x, currentTransform.position.y, targetTransform.position.z));
        }

        /// <summary>
        /// Reset a tranform to default values.
        /// </summary>
        public static void ResetTransform(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        /// <summary>
        /// Set active an object.
        /// </summary>
        public static void SetActive(this Transform transform, bool enable)
            => transform.gameObject.SetActive(enable);

        /// <summary> 
        /// Destroy children of an object. 
        /// </summary>
        public static void DestroyAllChildren(this GameObject gameObject)
        {
            foreach (var child in gameObject.transform.Cast<Transform>()) UnityEngine.Object.Destroy(child.gameObject);
        }
        public static void DestroyAllChildren(this Transform gameObject)
        {
            foreach (var child in gameObject.transform.Cast<Transform>()) UnityEngine.Object.Destroy(child.gameObject);
        }

        /// <summary>
        /// Destroy child at index
        /// </summary>
        public static void DestroyChildAt(this Transform transform, int index)
        {
            UnityEngine.Object.Destroy(transform.Cast<Transform>().ToList()[index]);
        }

        /// <summary>
        /// Set active all children of an object.
        /// </summary>
        public static void SetActiveAllChildren(this GameObject gameObject, bool enable)
        {
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.SetActive(enable);
            }
        }
        public static void SetActiveAllChildren(this Transform gameObject, bool enable)
        {
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.SetActive(enable);
            }
        }

        /// <summary>
        /// Change index of a child.
        /// </summary>
        public static void ChangeChildIndex(this Transform transform, int from, int to)
            => transform.GetChild(from).SetSiblingIndex(to);
        public static void MoveChildToFirst(this Transform transform, int index)
            => transform.GetChild(index).SetAsFirstSibling();
        public static void MoveChildToLast(this Transform transform, int index)
            => transform.GetChild(index).SetAsLastSibling();

        /// <summary>
        /// Set single axis of transform.
        /// </summary>
        public static void SetX(this Transform transform, float localZ, bool isLocal = false)
        {
            if (!isLocal) transform.position = transform.position.SetX(localZ);
            else transform.localPosition = transform.localPosition.SetX(localZ);
        }
        public static void SetY(this Transform transform, float localZ, bool isLocal = false)
        {
            if (!isLocal) transform.position = transform.position.SetY(localZ);
            else transform.localPosition = transform.localPosition.SetY(localZ);
        }
        public static void SetZ(this Transform transform, float localZ, bool isLocal = false)
        {
            if (!isLocal) transform.position = transform.position.SetZ(localZ);
            else transform.localPosition = transform.localPosition.SetZ(localZ);
        }
    }
}