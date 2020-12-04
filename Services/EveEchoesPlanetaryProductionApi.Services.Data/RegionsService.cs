namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Data;

    using Microsoft.EntityFrameworkCore;

    public class RegionsService : IRegionsService
    {
        private readonly EveEchoesPlanetaryProductionApiDbContext dbContext;

        public RegionsService(EveEchoesPlanetaryProductionApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> GetCount()
            => await this.dbContext.Regions.CountAsync();
    }
}