namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Auth;
    using EveEchoesPlanetaryProductionApi.Api.Models.Locations;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystemServiceModel;

    public interface IAppDataService
    {
        event Func<Task> OnStateChange;

        UserResponseModel User { get; set; }

        LocationModel Location { get; set; }

        SolarSystemServiceModel CurrentSolarSystem { get; set; }

        IDictionary<string, decimal> PlanetaryResourcesPrices { get; set; }

        int? RegionsCount { get; set; }

        int? ConstellationsCount { get; set; }

        int? SolarSystemCount { get; set; }

        int? PlanetaryResourcesCount { get; set; }

        public void StateHasChanged();
    }
}
