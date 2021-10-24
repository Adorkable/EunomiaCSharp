using System;
using System.Collections.Generic;
using System.Linq;

namespace Eunomia
{
    public static partial class IEnumerableExtensions
    {
        public static IEnumerable<string> FilterNullOrEmpty(this IEnumerable<string> enumerableToFilter)
        {
            return enumerableToFilter.FilterNot(String.IsNullOrEmpty);
        }

        public static IEnumerable<string> FilterIsNullOrWhiteSpace(this IEnumerable<string> enumerableToFilter)
        {
            return enumerableToFilter.FilterNot(String.IsNullOrWhiteSpace);
        }
    }
}