using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class ListExtensions
    {
        public static T Random<T>(this IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentException("Cannot randomly pick an item from the list, the list is null!");
            var enumerable = items as T[] ?? items.ToArray();
            if (!enumerable.Any()) throw new ArgumentException("Cannot randomly pick an item from the list, there are no items in the list!");
            var r = UnityEngine.Random.Range(0, enumerable.Count());
            return enumerable.ElementAt(r);
        }
    }
}