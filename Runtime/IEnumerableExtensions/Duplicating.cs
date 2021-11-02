using System;
using System.Collections.Generic;
using System.Linq;

namespace Eunomia
{
    public static partial class IEnumerableExtensions
    {
        // TODO: do we have to do ToList to actually create a copy? What is the fastest method
        public static IList<T> Clone<T>(this IEnumerable<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        // TODO: do we have to do ToList to actually create a copy? What is the fastest method
        public static IList<T> Copy<T>(this IEnumerable<T> listToCopy)
        {
            return listToCopy.Select(item => item).ToList();
        }
    }
}