using System;
using System.Collections.Generic;

namespace Eunomia
{
    public static class ListExtensions
    {
        public static void ForEach<T>(this IList<T> listToForEach, Action<T> action)
        {
            foreach (var element in listToForEach)
            {
                action(element);
            }
        }

        public static IEnumerable<T> RemoveAll<T>(this IList<T> listToRemoveFrom, IList<T> remove) where T : ICloneable
        {
            var result = listToRemoveFrom.Clone();
            remove.ForEach(value => { listToRemoveFrom.Remove(value); });
            return result;
        }

        /// <summary>
        /// Randomizes contents of list based on Fisher-Yates shuffle
        /// Based on: https://stackoverflow.com/a/1262619
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToRandomize">List to randomize</param>
        /// <returns>A reference to the original list, now randomized</returns>
        public static IList<T> Randomize<T>(this IList<T> listToRandomize, IRandom random = null)
        {
            var useRandom = random ?? IEnumerableExtensions.GetFallbackRandom();

            int length = listToRandomize.Count;
            while (length > 1)
            {
                length--;
                int k = useRandom.Next(length + 1);
                var value = listToRandomize[k];
                listToRandomize[k] = listToRandomize[length];
                listToRandomize[length] = value;
            }
            return listToRandomize;
        }
    }
}