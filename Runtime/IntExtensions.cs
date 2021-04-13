using System;

namespace Eunomia
{
    public static class IntExtensions
    {
        /// <summary>
        /// Wraps a value around 0 and a specified wrap around value
        /// </summary>
        /// <param name="value">Value to wrap</param>
        /// <param name="around">Value to use to wrap around, exclusive</param>
        /// <returns>Wrapped value</returns>
        public static int Wrap(this int value, int around)
        {
            if (value < 0)
            {
                return around + value;
            }
            else if (value >= around)
            {
                return value % around;
            }
            return value;
        }

        public static int ClampUpperExclusive(this int value, int within)
        {
            if (within == 0)
            {
                throw new OverflowException();
            }
            if (value < 0)
            {
                return 0;
            }
            else if (value >= within)
            {
                return within - 1;
            }
            return value;
        }
    };
};
