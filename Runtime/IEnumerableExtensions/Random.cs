using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace - In folder for organization reasons
namespace Eunomia
{
    // ReSharper disable once InconsistentNaming - extensions for IEnumerable :P
    public static partial class IEnumerableExtensions
    {
        private static IRandom fallbackRandom;

        public static IRandom GetFallbackRandom()
        {
            if (fallbackRandom != null)
            {
                return fallbackRandom;
            }

            fallbackRandom = new SystemRandomWrapper(
                new Random((int) new DateTime().TimeOfDay.TotalMilliseconds)
            );
            return fallbackRandom;
        }

        /// <summary>
        ///     Return a randomly selected index within `enumerable`
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="random"></param>
        /// <returns>A randomly selected index within `enumerable`</returns>
        /// <exception cref="System.ArgumentException">The enumerable has 0 elements in it</exception>
        public static int RandomIndex<T>(this IEnumerable<T> enumerable, IRandom random = null)
        {
            if (enumerable == null)
            {
                throw new ArgumentException("Provided null IEnumerable", nameof(enumerable));
            }

            if (!enumerable.Any())
            {
                throw new ArgumentException("Provided IEnumerable with 0 elements", nameof(enumerable));
            }

            var useRandom = random ?? GetFallbackRandom();

            return useRandom.Next(0, enumerable.Count());
        }

        /// <summary>
        ///     Return a randomly selected element within `enumerable`
        /// </summary>
        /// <param name="enumerable">Enumerable to return an element from</param>
        /// <param name="random">Optional System.Random random instance</param>
        /// <returns>A randomly selected element from within `enumerable`</returns>
        /// <exception cref="System.ArgumentException">The enumerable has 0 elements in it</exception>
        /// <exception cref="System.InvalidOperationException">
        ///     Internally used Eunomia.IEnumerableExtensions.RandomIndex method
        ///     returns an index out of range of our enumerable
        /// </exception>
        public static T RandomElement<T>(this IEnumerable<T> enumerable, IRandom random = null)
        {
            if (enumerable == null)
            {
                throw new ArgumentException("Provided null IEnumerable", nameof(enumerable));
            }

            if (!enumerable.Any())
            {
                throw new ArgumentException("Provided IEnumerable with 0 elements", nameof(enumerable));
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