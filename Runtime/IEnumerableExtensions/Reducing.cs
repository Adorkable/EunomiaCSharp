using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace - In folder for organization reasons
namespace Eunomia
{    
    // ReSharper disable once InconsistentNaming - extensions for IEnumerable :P
    public static partial class IEnumerableExtensions
    {
        // TODO: do for nullables
        public static readonly Func<int, object, int> NullCountReducer = (previous, test) =>
        {
            var testedCount = test == null ? 1 : 0;
            return previous + testedCount;
        };

        public static int NullCount(this IEnumerable<object> objects)
        {
            return objects.Reduce(NullCountReducer, 0);
        }

        public static readonly Func<int, object, int> NotNullCountReducer = (previous, test) =>
        {
            var testedCount = test != null ? 1 : 0;
            return previous + testedCount;
        };

        public static int NotNullCount(this IEnumerable<object> objects)
        {
            return objects.Reduce(NotNullCountReducer, 0);
        }
    }
}