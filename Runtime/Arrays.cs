using System;

namespace Eunomia
{
    public class Arrays
    {
        /// <summary>
        /// Create a new array with an element added to the end
        /// </summary>
        /// <param name="add">Element to add</param>
        /// <param name="to">Array to add to</param>
        /// <returns>New array with `add` param added to the end</returns>
        public static T[] AddToBack<T>(T add, T[] to)
        {
            T[] result = to;

            AddToBack(add, ref result);

            return result;
        }

        public static void AddToBack<T>(T add, ref T[] to)
        {
            Array.Resize(ref to, to.Length + 1);

            to[to.Length - 1] = add;
        }
    }
}