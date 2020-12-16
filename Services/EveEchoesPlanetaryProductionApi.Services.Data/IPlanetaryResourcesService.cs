namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.PlanetaryResources;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    public interface IPlanetaryResourcesService
    {
        Task<IEnumerable<PlanetaryResourceServiceModel>> GetBestPlanetaryResourcesInRangeAsync(long solarSystemId, PriceSelector priceSelector, int range, int resourcesCount);

        Task<IEnumerable<string>> GetAllPlanetaryResources();

        Task<IEnumerable<TOut>> GetAllPlanetaryResources<TOut>();

        Task<(int Count, IEnumerable<BestResourceServiceModel> Resources)> GetBestResourcesInConstellation(long constellationId, BestInputModel input);
    }
}
