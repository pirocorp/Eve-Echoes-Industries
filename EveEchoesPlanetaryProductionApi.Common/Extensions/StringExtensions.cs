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

        public static string Capitalize(this string str) => $"{char.ToUpper(str[0])}{str.Substring(1)}";

        public static string ToTitleCase(this string str)
        {
            var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");

            return new CultureInfo("en-US", false)
                .TextInfo
                .ToTitleCase(string.Join(" ", pattern.Matches(str)).ToLower());
        }
    }
}
