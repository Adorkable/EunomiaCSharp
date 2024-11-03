using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace - In folder for organization reasons
namespace Eunomia
{
    // ReSharper disable once InconsistentNaming - extensions for IEnumerable :P
    public static partial class IEnumerableExtensions
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> enumerableToFilter, Func<T, int, bool> predicate)
        {
            return enumerableToFilter.Where(predicate);
        }

        public static IEnumerable<T> FilterNot<T>(this IEnumerable<T> enumerableToFilter, Func<T, int, bool> predicate)
        {
            return enumerableToFilter.Where((element, index) => !predicate(element, index));
        }

        public static TReduced Reduce<T, TReduced>(this IEnumerable<T> enumerableToReduce,
            Func<TReduced, T, TReduced> reduce, TReduced start)
        {
            return enumerableToReduce.Aggregate(start, reduce);
        }

        public static IEnumerable<Q> Map<T, Q>(this IEnumerable<T> enumerableToMap, )
        {
            return enumerableToMap.Select();
        }

        public static IEnumerable<T> FlatMap<T>(this IEnumerable<IEnumerable<T>> enumerableToFlatten)
        {
            return enumerableToFlatten.SelectMany(value => value);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerableToOperateOn, Action<T> operation)
        {
            foreach (var operateOn in enumerableToOperateOn)
            {
                operation(operateOn);
            }
        }
    }
}