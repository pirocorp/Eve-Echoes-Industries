namespace EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetBestSolarSystemPlanetaryResourcesValues
{
    using System.ComponentModel.DataAnnotations;

    public class GetBestSolarSystemPlanetaryResourcesValuesInputModel
    {
        [Required]
        public string PriceSelector { get; set; }

        public int MiningPlanets { get; set; }
    }
}
