namespace EveEchoesPlanetaryProductionApi.Common.Extensions
{
    using System.Globalization;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string GetLastJump(this string route)
        {
            var jumps = route.Split(GlobalConstants.RoutePathDelimiter);

            return jumps[^1];
        }

        public static string RemoveSpaces(this string str) => str.Replace(" ", string.Empty);

        public static string Capitalize(this string str)
        {
            var index = str.LastIndexOf("/");

            str = str.Substring(index + 1);

            return $"{char.ToUpper(str[0])}{str.Substring(1)}";
        }

        public static string ToTitleCase(this string str)
        {
            var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");

            return new CultureInfo("en-US", false)
                .TextInfo
                .ToTitleCase(string.Join(" ", pattern.Matches(str)).ToLower());
        }

        public static string SplitWords(this string str)
            => Regex.Replace(str, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
    }
}
