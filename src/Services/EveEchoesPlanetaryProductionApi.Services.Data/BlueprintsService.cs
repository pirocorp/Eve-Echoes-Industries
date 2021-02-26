namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Data;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    using Microsoft.EntityFrameworkCore;

    public class BlueprintsService : IBlueprintsService
    {
        private readonly EveEchoesPlanetaryProductionApiDbContext dbContext;

        public BlueprintsService(EveEchoesPlanetaryProductionApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<TOut>> GetBlueprintTypesAsync<TOut>()
            => await this.dbContext.Blueprints
                .Select(b => b.ProductType)
                .Distinct()
                .To<TOut>()
                .ToListAsync();

        public async Task<TOut> GetBlueprintAsync<TOut>(string blueprintId)
            => await this.dbContext.Blueprints
                .Where(b => b.Id.Equals(blueprintId))
                .To<TOut>()
                .FirstOrDefaultAsync();

        public async Task<(int Total, IEnumerable<TOut> Results)> GetBlueprintsPageAsync<TOut>(IEnumerable<long> types, int page)
        {
            var query = this.dbContext.Blueprints
                .Where(b => types.Contains(b.ProductTypeId));

            return await GetBlueprintsAsync<TOut>(query, page);
        }

        public async Task<(int Total, IEnumerable<TOut> Results)> SearchAsync<TOut>(IEnumerable<long> types, string searchTerm, int page)
        {
            var query = this.dbContext.Blueprints
                .Where(b => types.Contains(b.ProductTypeId) && b.BlueprintItem.Name.ToLower().Contains(searchTerm.ToLower()));

            return await GetBlueprintsAsync<TOut>(query, page);
        }

        private static async Task<(int Total, IEnumerable<TOut> Results)> GetBlueprintsAsync<TOut>(IQueryable<Blueprint> query, int page)
        {
            var count = query.Count();

            var results = await query.OrderBy(b => b.BlueprintItem.Name)
                .To<TOut>()
                .Skip((page - 1) * GlobalConstants.Ui.BlueprintsPageSize)
                .Take(GlobalConstants.Ui.BlueprintsPageSize)
                .ToListAsync();

            return (count, results);
        }
    }
}
