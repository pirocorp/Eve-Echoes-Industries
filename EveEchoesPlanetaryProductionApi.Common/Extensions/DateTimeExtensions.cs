namespace EveEchoesPlanetaryProductionApi.Common.Extensions
{
    using System;

    public static class DateTimeExtensions
    {
        public static DateTime DateTimeFromUnixTimestamp(long seconds)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(seconds);

            return dateTime;
        }
    }
}
