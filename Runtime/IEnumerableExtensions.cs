using System;
using System.Collections.Generic;
using System.Linq;

namespace Eunomia
{
    public static class IEnumerableExtensions_Linq_Naming
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> enumerableToFilter, Func<T, int, bool> predicate)
        {
            return enumerableToFilter.Where(predicate);
        }

        public static TReduced Reduce<T, TReduced>(this IEnumerable<T> enumerableToReduce, Func<TReduced, T, TReduced> reduce, TReduced start)
        {
            return enumerableToReduce.Aggregate(start, reduce);
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