namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Services.Data.Models;

    public interface IPlanetaryResourcesService
    {
        Task<IEnumerable<string>> GetAllPlanetaryResources();

        Task<IEnumerable<TOut>> GetAllPlanetaryResources<TOut>();

        Task<(int Count, IEnumerable<BestResourceServiceModel> Resources)> GetBestResourcesInConstellation(long constellationId, BestInputModel input);

        Task<(int Count, IEnumerable<BestResourceServiceModel> Resources)> GetBestResourcesInRegion(long regionId, BestInputModel input);

        Task<(int Count, IEnumerable<BestResourceServiceModel> Resources)> GetBestResourcesInRange(int range, long solarSystemId, BestInputModel input);
    }
}
