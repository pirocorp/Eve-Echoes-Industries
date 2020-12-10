namespace EveEchoesPlanetaryProductionApi.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PricesModel
    {
        [Range(typeof(decimal), "1", "2000000000")]
        public decimal LusteringAlloy { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal SheenCompound { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal GleamingAlloy { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal CondensedAlloy { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal PreciousAlloy { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal MotleyCompound { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal FiberComposite { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal LucentCompound { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal OpulentCompound { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal GlossyCompound { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal CrystalCompound { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal DarkCompound { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal ReactiveGas { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal NobleGas { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal BaseMetals { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal HeavyMetals { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal NobleMetals { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal ReactiveMetals { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal ToxicMetals { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal IndustrialFibers { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal SupertensilePlastics { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal Polyaramids { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal Coolant { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal Condensates { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal ConstructionBlocks { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal Nanites { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal SilicateGlass { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal SmartfabUnits { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal HeavyWater { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal SuspendedPlasma { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal LiquidOzone { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal IonicSolutions { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal OxygenIsotopes { get; set; }

        [Range(typeof(decimal), "1", "2000000000")]
        public decimal Plasmoids { get; set; }
    }
}
