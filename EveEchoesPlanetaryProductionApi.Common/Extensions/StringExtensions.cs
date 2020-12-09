namespace EveEchoesPlanetaryProductionApi.Common.Extensions
{
    public static class StringExtensions
    {
        public static string GetLastJump(this string route)
        {
            var jumps = route.Split(GlobalConstants.RoutePathDelimiter);

            return jumps[^1];
        }

        public static string RemoveSpaces(this string str) => str.Replace(" ", string.Empty);

        public static string Capitalize(this string str) => $"{char.ToUpper(str[0])}{str.Substring(1)}";
    }
}
