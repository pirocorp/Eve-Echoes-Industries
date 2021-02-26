namespace EveEchoesPlanetaryProductionApi.Api.Models.Blueprints
{
    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class BlueprintListingModel : IMapFrom<Blueprint>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression blueprint)
        {
            blueprint
                .CreateMap<Blueprint, BlueprintListingModel>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.BlueprintItem.Name));
        }
    }
}
