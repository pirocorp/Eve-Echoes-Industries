namespace EveEchoesPlanetaryProductionApi.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(Data.EveEchoesPlanetaryProductionApiDbContext dbContext, IServiceProvider serviceProvider);
    }
}
