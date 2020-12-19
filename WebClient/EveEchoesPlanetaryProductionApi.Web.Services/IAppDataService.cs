namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Auth;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystemServiceModel;

    public interface IAppDataService
    {
        public event Func<Task> OnStateChange;

        public SolarSystemServiceModel CurrentSolarSystem { get; set; }

        public int? RegionsCount { get; set; }

        public int? ConstellationsCount { get; set; }

        public int? SolarSystemCount { get; set; }

        public int? PlanetaryResourcesCount { get; set; }

        IDictionary<string, decimal> PlanetaryResourcesPrices { get; set; }

        UserResponseModel User { get; set; }

        public void StateHasChanged();
    }
}
