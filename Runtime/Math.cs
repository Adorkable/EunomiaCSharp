using System;

namespace Eunomia
{
    public static class Math
    {
        public static (float, float) FitWithAspect((float, float) content, (float, float) match)
        {
            var ratio = content.Item1 / content.Item2;
            var ratioMatch = match.Item1 / match.Item2;

            float newWidth, newHeight;
            if (ratioMatch > ratio)
            {
                newWidth = content.Item1 * match.Item2 / content.Item2;
                newHeight = match.Item2;
            }
            else
            {
                newWidth = match.Item1;
                newHeight = content.Item2 * match.Item1 / content.Item1;
            }
            return (newWidth, newHeight);
        }
    }
}
