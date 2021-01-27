namespace EveEchoesPlanetaryProductionApi.Api.Models.Blueprints
{
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class BlueprintTypeModel : IMapFrom<ItemType>
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
