namespace EveEchoesPlanetaryProductionApi.Api.Models.Blueprints.GetBlueprint
{
    using System.Collections.Generic;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class GetBlueprintModel : IMapFrom<Blueprint>, IHaveCustomMappings
    {
        public long BlueprintItemId { get; set; }

        public int TechLevel { get; set; }

        public long ProductionCost { get; set; }

        public long ProductionTime { get; set; }

        public long ProductionCount { get; set; }

        public string ProductType { get; set; }

        public IEnumerable<GetBlueprintModelResources> Resources { get; set; }

        public GetBlueprintModelProduct Product { get; set; }

        public void CreateMappings(IProfileExpression blueprint)
        {
            blueprint
                .CreateMap<Blueprint, GetBlueprintModel>()
                .ForMember(d => d.ProductType, opt => opt.MapFrom(s => s.ProductType.Name));
        }
    }
}
