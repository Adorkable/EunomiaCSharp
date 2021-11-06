using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace - In folder for organization reasons
namespace Eunomia
{
    // ReSharper disable once InconsistentNaming - extensions for IEnumerable :P
    public static partial class IEnumerableExtensions
    {
        public static bool EqualContents<T>(this IEnumerable<T>[] array, IEnumerable<T>[] compareTo)
            where T : IEquatable<T>
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