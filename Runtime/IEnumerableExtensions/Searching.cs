using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Eunomia
{
    // ReSharper disable once InconsistentNaming - extensions for IEnumerable :P
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// </summary>
        /// <returns>Found matcing item or default(<typeparamref name="T" />) and whether a match was found</returns>
        /// <param name="enumerableToOperateOn"></param>
        /// <param name="operation">Return `true` to return the current item, return `false` to continue</param>
        public static Found<T> Find<T>(this IEnumerable<T> enumerableToOperateOn, Func<T, int, bool> operation)
        {
            var index = 0;
            foreach (var operateOn in enumerableToOperateOn)
            {
                if (operation(operateOn, index))
                {
                    return new Found<T>()
                    {
                        index = index,
                        result = operateOn,
                        found = true
                    };
                }

                index += 1;
            }

            return new Found<T>() {result = default, found = false};
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> enumerableToFilter, Func<T, bool> predicate)
        {
            return enumerableToFilter.Filter((element, index) => { return predicate(element); });
        }

        public static IEnumerable<T> FilterNot<T>(this IEnumerable<T> enumerableToFilter, Func<T, bool> predicate)
        {
            return enumerableToFilter.FilterNot((element, index) => { return predicate(element); });
        }

        public struct Found<T>
        {
            public int index;
            public T result;
            public bool found;
        };
    }
}