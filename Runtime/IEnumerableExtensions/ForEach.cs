using System;
using System.Collections.Generic;

namespace Eunomia
{
    public static partial class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerableToOperateOn, Action<T, int> operation)
        {
            var index = 0;
            foreach (var operateOn in enumerableToOperateOn)
            {
                operation(operateOn, index);
                index += 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation">Return `false` to continue iteration, `true` to stop</param>
        public static void ForEach<T>(this IEnumerable<T> enumerableToOperateOn, Func<T, bool> operation)
        {
            foreach (var operateOn in enumerableToOperateOn)
            {
                if (operation(operateOn))
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation">Return `false` to continue iteration, `true` to stop</param>
        public static void ForEach<T>(this IEnumerable<T> enumerableToOperateOn, Func<T, int, bool> operation)
        {
            var index = 0;
            foreach (var operateOn in enumerableToOperateOn)
            {
                if (operation(operateOn, index))
                {
                    return;
                }
                index += 1;
            }
        }
    }
}