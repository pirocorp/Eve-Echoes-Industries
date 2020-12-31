namespace EveEchoesPlanetaryProductionApi.Api.Models.Regions.GetSimpleDetails
{
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class RegionSimpleDetailsModel : IMapFrom<Region>
    {
        public string Name { get; set; }
    }
}
