using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Eunomia
{
    // ReSharper disable once InconsistentNaming - extensions for IEnumerable :P
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