using System.Collections.Generic;
using System.Linq;
using System;

namespace YNL.Extensions.Methods
{
    public static class MArray
    {
        /// <summary> 
        /// Get random element in a array. 
        /// </summary>
        public static T GetRandom<T>(this T[] array)
        {
            if (array.Length == 0) throw new IndexOutOfRangeException("Array is empty.");
            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        /// <summary> 
        /// Get amount of elements satisfy the condition in array. 
        /// </summary>
        public static int Count<T>(this T[] list, Func<T, bool> predicate)
            => list.Where(predicate).ToArray().Length;

        /// <summary>
        /// Shuffle an array.
        /// </summary>
        public static void Shuffle<T>(this T[] list)
        {
            for (var i = list.Length - 1; i > 1; i--)
            {
                var j = UnityEngine.Random.Range(0, i + 1);
                var value = list[j];
                list[j] = list[i];
                list[i] = value;
            }
        }

        /// <summary>
        /// Return a array satisfies multiple predicates.
        /// </summary>
        public static IList<T> Wheres<T>(this T[] list, IList<Func<T, bool>> predicate)
        {
            List<T> newList = new List<T>();

            foreach (var item in predicate)
            {
                List<T> addList = list.Where(item).ToList();
                foreach (var element in addList) newList.Add(element);
            }

            return newList;
        }

        /// <summary>
        /// Check if an array is empty or not.
        /// </summary>
        public static bool IsEmpty<T>(this T[] array)
            => !array.IsNull() && array.Length <= 0 ? true : false;

        /// <summary>
        /// Swap 2 elements in an array.
        /// </summary>
        public static T[] Swap<T>(this T[] array, int indexA, int indexB)
        {
            T tmp = array[indexA];
            array[indexA] = array[indexB];
            array[indexB] = tmp;
            return array;
        }

        /// <summary>
        /// Add a value to an array;
        /// </summary>
        public static T[] Add<T>(this T[] array, T value)
        {
            List<T> list = array.ToList();
            list.Add(value);
            array = list.ToArray();
            return array;
        }

        /// <summary>
        /// Get index of an element in an array.
        /// </summary>
        public static int IndexOf<T>(this T[] array, T element)
            => Array.IndexOf(array, element);

        /// <summary>
        /// Try get an element from an array.
        /// </summary>
        public static T TryGet<T>(this T[] array, int index)
        {
            if (array.IsNullOrEmpty()) return default;
            else return array[index];
        }
    }
}