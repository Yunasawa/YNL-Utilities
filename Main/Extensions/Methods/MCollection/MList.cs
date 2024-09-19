using System.Collections.Generic;
using System;
using System.Linq;

namespace YNL.Extensions.Methods
{
    public static class MList
    {
        /// <summary>
        /// Get a list that removed the predicated elements.
        /// </summary>
        public static List<T> RemoveAllWhere<T>(this List<T> list, Predicate<T> predicate)
        {
            List<T> newList = new List<T>();
            foreach (var item in list) if (!predicate(item)) newList.Add(item);
            return newList;
        }

        /// <summary> 
        /// Get random element in a list. 
        /// </summary>
        public static T GetRandom<T>(this IList<T> list)
        {
            if (list.Count == 0) throw new IndexOutOfRangeException("List is empty!");
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Get random element in a list but not the same with current.
        /// </summary>
        public static T GetRandomNotRepeat<T>(this IList<T> list, int current)
        {
            if (list.Count == 0) throw new IndexOutOfRangeException("List is empty!");
            int next = UnityEngine.Random.Range(0, list.Count);
            if (next == current) return list.GetRandomNotRepeat(current);
            else return list[next];
        }

        /// <summary>
        /// Remove random element in a list. Return that element.
        /// </summary>
        public static T RemoveRandom<T>(this IList<T> list)
        {
            if (list.Count == 0) throw new IndexOutOfRangeException("List is empty!");
            int index = UnityEngine.Random.Range(0, list.Count);
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }

        /// <summary>
        /// Shuffle a list.
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            for (var i = list.Count - 1; i > 1; i--)
            {
                var j = UnityEngine.Random.Range(0, i + 1);
                var value = list[j];
                list[j] = list[i];
                list[i] = value;
            }
        }

        /// <summary>
        /// Return a list satisfies multiple predicates. Must use predicates which their set is not duplicated with others.
        /// </summary>
        public static List<T> Wheres<T>(this IList<T> list, params Func<T, bool>[] predicate)
        {
            List<T> newList = list.ToList();

            foreach (var item in predicate) newList = newList.Where(item).ToList();

            return newList;
        }

        /// <summary>
        /// Check if this item is the last item in list.
        /// </summary>
        public static bool IsLast<T>(this IList<T> list, T item)
            => item.Equals(list[^1]);

        /// <summary>
        /// Check if a list is null or not.
        /// </summary>
        public static bool IsNull<T>(this IList<T> list)
            => list == null ? true : false;

        /// <summary>
        /// Check if a list is empty or not.
        /// </summary>
        public static bool IsEmpty<T>(this IList<T> list)
            => !list.IsNull() && list.Count <= 0 ? true : false;

        /// <summary>
        /// Check if a list is null or empty or not.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IList<T> list)
            => list.IsNull() || list.IsEmpty() ? true : false;

        /// <summary>
        /// Add element if list doesn't contain it.
        /// </summary>
        public static IList<T> AddDistinct<T>(this IList<T> list, T element)
        {
            if (!list.Contains(element)) list.Add(element);
            return list;
        }

        /// <summary>
        /// Try to get an element from a list.
        /// </summary>
        public static T TryGet<T>(this List<T> list, int index)
        {
            if (index >= list.Count) return default;
            return list[index];
        }

        /// <summary>
        /// Try to remove element(s) from a list.
        /// </summary>
        public static IList<T> TryRemove<T>(this IList<T> list, T element)
        {
            if (list.Contains(element)) list.Remove(element);
            else MDebug.Caution("List does not contains element");
            return list;
        }
        public static IList<T> TryRemove<T>(this IList<T> list, params T[] elements)
        {
            foreach (var element in elements) list.TryRemove(element);
            return list;
        }

        /// <summary>
        /// Move an item from list1 into list2.
        /// </summary>
        public static void Move<T>(this List<T> list1, List<T> list2, T item)
        {
            list2.Add(item);
            list1.Remove(item);
        }

        /// <summary>
        /// Move an item in a list to another index.
        /// </summary>
        public static void MoveToIndex<T>(this List<T> list, int oldIndex, int newIndex)
        {
            if ((oldIndex == newIndex) || (0 > oldIndex) || (oldIndex >= list.Count) || (0 > newIndex) || (newIndex >= list.Count)) return;

            var i = 0;
            T tmp = list[oldIndex];

            if (oldIndex < newIndex)
            {
                for (i = oldIndex; i < newIndex; i++) list[i] = list[i + 1];
            }
            else
            {
                for (i = oldIndex; i > newIndex; i--) list[i] = list[i - 1];
            }
            list[newIndex] = tmp;
        }
        public static void MoveToFirst<T>(this List<T> list, T item)
        {
            int index = list.IndexOf(item);
            if (index <= 0 || index >= list.Count) return;
            list.MoveToIndex(index, 0);
        }
        public static void MoveToLast<T>(this List<T> list, T item)
        {
            int index = list.IndexOf(item);
            if (index < 0 || index >= list.Count) return;
            list.MoveToIndex(index, list.Count - 1);
        }
    }
}