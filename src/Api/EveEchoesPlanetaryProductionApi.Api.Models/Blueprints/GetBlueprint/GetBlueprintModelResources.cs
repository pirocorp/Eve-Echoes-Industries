namespace EveEchoesPlanetaryProductionApi.Api.Models.Blueprints.GetBlueprint
{
    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class GetBlueprintModelResources : IMapFrom<BlueprintResource>, IHaveCustomMappings
    {
        public long ResourceId { get; set; }

        public string ResourceName { get; set; }

        public long Quantity { get; set; }

        public void CreateMappings(IProfileExpression resource)
        {
            resource
                .CreateMap<BlueprintResource, GetBlueprintModelResources>()
                .ForMember(d => d.ResourceId, opt => opt.MapFrom(s => s.ItemId))
                .ForMember(d => d.ResourceName, opt => opt.MapFrom(s => s.Item.Name));
        }
    }
}
