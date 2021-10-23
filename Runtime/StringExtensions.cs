using System;
using System.Linq;
using System.Text.RegularExpressions;

public static class StringExtensions
{
    // TODO: add support for </> and other characters within "..."
    static Lazy<Regex> XmlTag = new Lazy<Regex>(() =>
    {
        return new Regex(@"([<][/]?[a-zA-Z="" ]*[>])", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    });

    public static string RemoveXMLTags(this string clean)
    {
        if (clean == null)
        {
            return null;
        }

        MatchCollection matches = XmlTag.Value.Matches(clean);

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
