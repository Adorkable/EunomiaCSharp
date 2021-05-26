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

        private static System.Random fallbackRandom = null;

        private static System.Random GetFallbackRandom()
        {
            if (fallbackRandom != null)
            {
                return fallbackRandom;
            }

            fallbackRandom = new System.Random((int)(new DateTime()).TimeOfDay.TotalMilliseconds);
            return fallbackRandom;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        /// <exception cref="System.IndexOutOfRangeException">If the enumerable has 0 elements in it</exception>
        public static int RandomIndex<T>(this IEnumerable<T> enumerable, System.Random random = null)
        {
            if (enumerable.Count() <= 0)
            {
                throw new IndexOutOfRangeException("Provided IEnumerable with 0 elements");
            }

            System.Random useRandom;
            if (random == null)
            {
                useRandom = GetFallbackRandom();
            }
            else
            {
                useRandom = random;
            }

            return useRandom.Next(0, enumerable.Count());
        }

        /// <summary>
        /// Return a randomly selected element within our enumerable
        /// </summary>
        /// <param name="enumerable">Enumerable to return an element from</param>
        /// <param name="random">Optional System.Random random instance</param>
        /// <returns>Randomly selected element from within our enumerable</returns>
        /// <exception cref="System.IndexOutOfRangeException">If the enumerable has 0 elements in it</exception>
        /// <exception cref="System.InvalidOperationException">If internally used Eunomia.IEnumerableExtensions_Linq_Naming.RandomIndex method returns an index out of range of our enumerable</exception>
        public static T RandomElement<T>(this IEnumerable<T> enumerable, System.Random random = null)
        {
            if (enumerable.Count() <= 0)
            {
                throw new IndexOutOfRangeException("Provided IEnumerable with 0 elements");
            }

            var index = RandomIndex(enumerable, random);
            if (index >= enumerable.Count())
            {
                throw new InvalidOperationException("RandomIndex returned an index out of range");
            }

            return enumerable.ElementAt(index);
        }
    }
}