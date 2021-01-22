namespace EveEchoesPlanetaryProductionApi.Api.Models.Blueprints.GetBlueprint
{
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class GetBlueprintModelProduct : IMapFrom<Item>
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
