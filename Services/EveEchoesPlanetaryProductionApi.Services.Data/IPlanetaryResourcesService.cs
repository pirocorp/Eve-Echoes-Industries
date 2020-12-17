namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Services.Data.Models;

    public interface IPlanetaryResourcesService
    {
        Task<IEnumerable<string>> GetResourcesListAsync();

        Task<IEnumerable<TOut>> GetResourcesAsync<TOut>();

        Task<(int Count, IEnumerable<BestResourceServiceModel> Resources)> GetBestResourcesInConstellationAsync(long constellationId, InputModel input);

        Task<(int Count, IEnumerable<BestResourceServiceModel> Resources)> GetBestResourcesInRegionAsync(long regionId, InputModel input);

        Task<(int Count, IEnumerable<BestResourceServiceModel> Resources)> GetBestResourcesInRangeAsync(int range, long solarSystemId, InputModel input);
    }
}
