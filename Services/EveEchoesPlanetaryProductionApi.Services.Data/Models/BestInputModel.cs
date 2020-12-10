namespace EveEchoesPlanetaryProductionApi.Services.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    public class BestInputModel
    {
        public BestInputModel()
        {
            this.Page = 1;
        }

        [Required]
        public string Price { get; set; }

        public int MiningPlanets { get; set; }

        [Range(1, int.MaxValue)]
        public int Page { get; set; }

        public PricesModel Prices { get; set; }

        [NotMapped]
        public PriceSelector PriceSelector { get; set; }
    }
}
