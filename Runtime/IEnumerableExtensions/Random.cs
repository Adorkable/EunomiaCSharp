using System;
using System.Collections.Generic;
using System.Linq;

namespace Eunomia
{
    public static partial class IEnumerableExtensions
    {
        // TODO: use IRandom interface
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