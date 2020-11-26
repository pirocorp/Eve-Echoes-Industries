namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Data;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class SolarSystemService : ISolarSystemService
    {
        private readonly EveEchoesPlanetaryProductionApiDbContext dbContext;

        public SolarSystemService(EveEchoesPlanetaryProductionApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TOut> GetById<TOut>(long id)
            => await this.Get<TOut>(ss => ss.Id.Equals(id));

        public async Task<TOut> GetByName<TOut>(string name)
            => await this.Get<TOut>(ss => ss.Name.Equals(name));

        private async Task<TOut> Get<TOut>(Expression<Func<SolarSystem, bool>> predicate)
            => await this.dbContext.SolarSystems
                .Where(predicate)
                .To<TOut>()
                .FirstOrDefaultAsync();
    }
}