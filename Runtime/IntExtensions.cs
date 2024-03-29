using System;

namespace Eunomia
{
    public static class IntExtensions
    {
        /// <summary>
        ///     Wraps a value around 0 (inclusive) and a specified wrap around value (exclusive)
        /// </summary>
        /// <param name="value">Value to wrap</param>
        /// <param name="around">Value to use to wrap around, exclusive</param>
        /// <returns>Wrapped value</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">`around` &lt;= 0</exception>
        public static int Wrap(this int value, int around)
        {
            if (around <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(around), around, "around must be >= 0");
            }

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

        /// <summary>
        ///     Restricts a value to be within `lower` and `upper` (inclusive)
        /// </summary>
        /// <param name="value">Value to wrap</param>
        /// <param name="lower">Lower value to be within, inclusive</param>
        /// <param name="upper">Upper value to be within, inclusive</param>
        /// <returns>`this` updated</returns>
        /// <exception cref="System.ArgumentException">`lower` &gt; `upper`</exception>
        public static int Clamp(this int value, int lower, int upper)
        {
            if (lower > upper)
            {
                throw new ArgumentException($"lower ({lower}) must be less than or equal to upper ({upper})",
                    nameof(lower));
            }

            if (value < lower)
            {
                return lower;
            }
            else if (value > upper)
            {
                return upper;
            }

            return value;
        }

        /// <exception cref="System.ArgumentOutOfRangeException">`within` &lt;= 0</exception>
        public static int ClampUpperExclusive(this int value, int within)
        {
            if (within <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(within), within, "within must be > 0");
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

        /// <summary>
        ///     Map a value from one range to another. Supports end values that are less than their start values
        /// </summary>
        /// <param name="value">Value to map</param>
        /// <param name="fromStart">From range start</param>
        /// <param name="fromEnd">From range end</param>
        /// <param name="toStart">To range start</param>
        /// <param name="toEnd">To range end</param>
        /// <returns>`this` updated</returns>
        public static int Map(this int value,
            int fromStart,
            int fromEnd,
            int toStart,
            int toEnd
        )
        {
            if (fromStart == fromEnd)
            {
                throw new ArgumentException($"fromStart ({fromStart}) must not equal fromEnd ({fromEnd})",
                    nameof(fromStart));
            }

            if (toStart == toEnd)
            {
                throw new ArgumentException($"toStart ({toStart}) must not equal toEnd ({toEnd})", nameof(toStart));
            }

            return (int) (
                toStart + (float) (value - fromStart) / (float) (fromEnd - fromStart) * (toEnd - toStart)
            );
        }
    };
};