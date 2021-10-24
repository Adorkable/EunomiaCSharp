using System;
using System.Collections.Generic;

namespace Eunomia
{
    public static class ListExtensions
    {
        public static void ForEach<T>(this IList<T> listToForEach, Action<T> action)
        {
            foreach (T element in listToForEach)
            {
                action(element);
            }
        }

        public static IEnumerable<T> RemoveAll<T>(this IList<T> listToRemoveFrom, IList<T> remove) where T : ICloneable
        {
            var result = listToRemoveFrom.Clone();
            remove.ForEach(value =>
            {
                listToRemoveFrom.Remove(value);
            });
            return result;
        }
    }
}
