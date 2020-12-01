namespace EveEchoesPlanetaryProductionApi.Common.Extensions
{
    public static class StringExtensions
    {
        public static string GetLastJump(this string route)
        {
            var jumps = route.Split(GlobalConstants.RoutePathDelimiter);

            return jumps[^1];
        }
    }
}
