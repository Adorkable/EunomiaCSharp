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

    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerableToOperateOn, Action<T, int> operation)
        {
            var index = 0;
            foreach (var operateOn in enumerableToOperateOn)
            {
                operation(operateOn, index);
                index += 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation">Return `false` to continue iteration, `true` to stop</param>
        public static void ForEach<T>(this IEnumerable<T> enumerableToOperateOn, Func<T, bool> operation)
        {
            foreach (var operateOn in enumerableToOperateOn)
            {
                if (operation(operateOn))
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation">Return `false` to continue iteration, `true` to stop</param>
        public static void ForEach<T>(this IEnumerable<T> enumerableToOperateOn, Func<T, int, bool> operation)
        {
            var index = 0;
            foreach (var operateOn in enumerableToOperateOn)
            {
                if (operation(operateOn, index))
                {
                    return;
                }
                index += 1;
            }
        }

        public struct Found<T>
        {
            public int index;
            public T result;
            public bool found;
        };

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Found matcing item or default(<typeparamref name="T"/>) and whether a match was found</returns>
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
            return new Found<T>() { result = default(T), found = false };
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> enumerableToFilter, Func<T, bool> predicate)
        {
            return Eunomia.IEnumerableExtensions_Linq_Naming.Filter(enumerableToFilter, (element, index) =>
            {
                return predicate(element);
            });
        }

        public static bool EqualContents<T>(this IEnumerable<T>[] array, IEnumerable<T>[] compareTo) where T : IEquatable<T>
        {
            if (array == null && compareTo == null)
            {
                return true;
            }

            if (array == null || compareTo == null)
            {
                return false;
            }

            if (array.Length != compareTo.Length)
            {
                return false;
            }

            for (var index = 0; index < array.Length; index++)
            {
                if (array[index].Equals(compareTo[index]) == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}