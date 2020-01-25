using System;
using System.Collections.Generic;
using System.Linq;

namespace Eunomia
{
    public static class ListExtensions
    {
        // based on https://stackoverflow.com/a/222640https://stackoverflow.com/a/222640
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static IList<T> Copy<T>(this IList<T> listToCopy)
        {
            return listToCopy.Select(item => item).ToList();
        }

        public static void ForEach<T>(this IList<T> listToForEach, Action<T> action)
        {
            foreach (T element in listToForEach)
            {
                action(element);
            }
        }

        public static IList<T> RemoveAll<T>(this IList<T> listToRemoveFrom, IList<T> remove) where T : ICloneable
        {
            var result = listToRemoveFrom.Clone();
            remove.ForEach(value => listToRemoveFrom.Remove(value));
            return result;
        }
    }
}
