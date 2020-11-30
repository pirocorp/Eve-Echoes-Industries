namespace EveEchoesPlanetaryProductionApi.Api.Infrastructure.JsonNamingPolicies
{
    using System.Text.Json;

    using EveEchoesPlanetaryProductionApi.Api.Infrastructure.Extensions;

    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public static SnakeCaseNamingPolicy Instance { get; } = new SnakeCaseNamingPolicy();

        public override string ConvertName(string name)
        {
            return name.ToSnakeCase();
        }
    }
}
