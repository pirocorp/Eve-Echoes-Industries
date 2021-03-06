﻿namespace EveEchoesPlanetaryProductionApi.Services.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    public class InputModel
    {
        public InputModel()
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
        [JsonIgnore]
        public PriceSelector PriceSelector { get; set; }
    }
}
