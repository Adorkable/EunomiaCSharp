using System;

public static class FloatExtensions
{
    /// <summary>
    /// Wraps a value around 0 and a specified wrap around value
    /// </summary>
    /// <param name="value">Value to wrap</param>
    /// <param name="around">Value to use to wrap around, exclusive</param>
    /// <returns></returns>
    public static float Wrap(this float value, float around)
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

    public static float Clamp(this float value, float lower, float upper)
    {
        if (upper < lower)
        {
            throw new OverflowException();
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

    public static float Map(this float value,
                            float fromStart,
                            float fromEnd,
                            float toStart,
                            float toEnd
                            )
    {
        return (
          toStart + ((value - fromStart) / (fromEnd - fromStart)) * (toEnd - toStart)
        );
    }
}