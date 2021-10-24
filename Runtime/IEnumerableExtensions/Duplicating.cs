using System;
using System.Collections.Generic;
using System.Linq;

namespace Eunomia
{
    public static partial class IEnumerableExtensions
    {
        public static IList<T> Clone<T>(this IEnumerable<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static IList<T> Copy<T>(this IEnumerable<T> listToCopy)
        {
            return listToCopy.Select(item => item).ToList();
        }
    }
}