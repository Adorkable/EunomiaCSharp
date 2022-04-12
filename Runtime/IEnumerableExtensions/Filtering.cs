using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace - In folder for organization reasons
namespace Eunomia
{    
    // ReSharper disable once InconsistentNaming - extensions for IEnumerable :P
    public static partial class IEnumerableExtensions
    {
        // TODO: do for nullables
        public static bool NotNullTest<T>(T test) where T : class
        {
            return test != null;
        }

        public static IEnumerable<T> OnlyNonNull<T>(this IEnumerable<T> objects) where T : class
        {
            return objects.Filter(NotNullTest);
        }
    }
}