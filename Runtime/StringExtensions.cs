using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Eunomia
{
    public static class StringExtensions
    {
        // TODO: add support for </> and other characters within "..."
        public static readonly Lazy<Regex> XmlTag = new Lazy<Regex>(
            () => new Regex(@"([<][/]?[a-zA-Z="" ]*[>])", RegexOptions.Compiled | RegexOptions.IgnoreCase)
        );

        public static string RemoveXmlTags(this string clean)
        {
            if (clean == null)
            {
                return null;
            }

            var matches = XmlTag.Value.Matches(clean);

            var result = clean;
            for (var index = matches.Count - 1; index >= 0; index--)
            {
                var match = matches[index];
                var start = match.Groups[0].Index;
                var matchedString = match.Groups[0].Value;
                result = result.Remove(start, matchedString.Count());
            }

            return result;
        }
    }
}