namespace EveEchoesPlanetaryProductionApi.Api.Models.Items.GetBestPlanetaryResourcesInRange
{
    public class GetBestPlanetaryResourcesInRangeInputModel
    {
        public string PriceSelector { get; set; }

        public int MiningPlanets { get; set; }

        public int Range { get; set; }
    }
}
