using System.Collections.Generic;
using System.Linq;

namespace Eunomia
{
    public static class ListOfStringsExtensions
    {
        // based on https://stackoverflow.com/a/222640https://stackoverflow.com/a/222640
        public static List<string> FilterEmpty(this IList<string> listToFilter)
        {
            return listToFilter.Where(value => value.Length > 0).ToList();
        }
    }
}