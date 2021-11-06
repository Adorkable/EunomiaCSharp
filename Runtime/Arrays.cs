using System;

namespace Eunomia
{
    public class Arrays
    {
        /// <summary>
        ///     Create a new array with an element added to the end
        /// </summary>
        /// <param name="add">Element to add</param>
        /// <param name="to">Array which will be copied and whose copy will be added to</param>
        /// <returns>New array with `add` param added to the end</returns>
        public static T[] AddToBack<T>(T add, T[] to)
        {
            var result = to;

            AddToBack(add, ref result);

            return result;
        }

        /// <summary>
        ///     Resizes array `to` and adds element to the end
        /// </summary>
        /// <param name="add">Element to add</param>
        /// <param name="to">Array that will be resized and added to</param>
        public static void AddToBack<T>(T add, ref T[] to)
        {
            Array.Resize(ref to, to.Length + 1);

            to[^1] = add;
        }
    }
}