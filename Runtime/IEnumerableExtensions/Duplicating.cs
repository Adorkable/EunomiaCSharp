using System;
using System.Collections.Generic;
using System.Linq;

namespace Eunomia
{
    public static partial class IEnumerableExtensions
    {
        public static IEnumerable<T> Clone<T>(this IEnumerable<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone());
        }

        public static IEnumerable<T> Copy<T>(this IEnumerable<T> listToCopy)
        {
            return listToCopy.Select(item => item);
        }
    }
}