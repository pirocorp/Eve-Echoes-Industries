namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Auth;
    using EveEchoesPlanetaryProductionApi.Api.Models.Locations;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystemServiceModel;

    public class AppDataService : IAppDataService
    {
        public AppDataService()
        {
            this.PlanetaryResourcesPrices = new Dictionary<string, decimal>();
        }

        public event Func<Task> OnStateChange;

        public UserResponseModel User { get; set; }

        public LocationModel Location { get; set; }

        public SolarSystemServiceModel CurrentSolarSystem { get; set; }

        public IDictionary<string, decimal> PlanetaryResourcesPrices { get; set; }

        public int? RegionsCount { get; set; }

        public int? ConstellationsCount { get; set; }

        public int? SolarSystemCount { get; set; }

        public int? PlanetaryResourcesCount { get; set; }

        public void StateHasChanged() => this.OnStateChange?.Invoke();
    }
}
