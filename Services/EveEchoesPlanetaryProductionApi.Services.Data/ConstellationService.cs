namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Data;
    using Microsoft.EntityFrameworkCore;

    public class ConstellationService : IConstellationService
    {
        private readonly EveEchoesPlanetaryProductionApiDbContext dbContext;

        public ConstellationService(EveEchoesPlanetaryProductionApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> GetCount()
            => await this.dbContext.Constellations.CountAsync();
    }
}