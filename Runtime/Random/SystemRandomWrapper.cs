namespace Eunomia
{
    public class SystemRandomWrapper : IRandom
    {
        protected System.Random random;

        /// <summary>
        /// Instantiates a new `SystemRandomWrapper` instance wrapping a `System.Random`.
        /// </summary>
        /// <param name="random">System.Random instance to wrap</param>
        public SystemRandomWrapper(System.Random random)
        {
            this.random = random;
        }

        /// <summary>
        /// Returns a non-negative random integer.
        /// </summary>
        /// <returns>A 32-bit signed integer that is greater than or equal to 0 and less than int.MaxValue.</returns>
        int IRandom.Next()
        {
            return random.Next();
        }

        /// <summary>
        /// Returns a non-negative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. `maxValue` must be greater than or equal to 0.</param>
        /// <returns>A 32-bit signed integer that is greater than or equal to 0, and less than `maxValue`; that is, the range of return values ordinarily includes 0 but not `maxValue`. However, if `maxValue` equals 0, maxValue is returned.</returns>
        int IRandom.Next(int maxValue)
        {
            return random.Next(maxValue);
        }

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. `maxValue` must be greater than or equal to `minValue`.</param>
        /// <returns>A signed integer greater than or equal to `minValue` and less than `maxValue`; that is, the range of return values includes `minValue` but not `maxValue`. If `minValue` equals `maxValue`, `minValue` is returned.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">`minValue` is greater than `maxValue`.</exception>
        int IRandom.Next(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
    }
}