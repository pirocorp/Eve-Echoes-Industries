namespace EveEchoesPlanetaryProductionApi.Web.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.BestSystemModel;
    using EveEchoesPlanetaryProductionApi.Common.Extensions;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;
    using EveEchoesPlanetaryProductionApi.Web.Services;

    using Microsoft.AspNetCore.Components;

    public abstract class BestSystemBase : ComponentBase
    {
        [Inject] 
        protected IAppDataService AppDataService { get; set; }

        private bool IsSelected { get; set; }

        protected bool ShowLoader { get; set; }

        private int MiningPlanets { get; set; }

        protected PriceSelector PriceSelector { get; set; }

        protected IEnumerable<BestSystemModel> BestSystems { get; set; }

        private PricesModel Prices { get; set; }

        protected BestInputModel UserInput { get; set; }

        protected abstract Task LoadData();

        protected void CreateInputModelFromUserInput()
        {
            var model = new BestInputModel()
            {
                MiningPlanets = this.MiningPlanets,
                Price = this.PriceSelector.ToString(),
                Prices = null
            };

            if (this.PriceSelector is PriceSelector.UserProvided)
            {
                model.Prices = this.Prices;
            }

            this.UserInput = model;
        }

        protected async Task OnPriceSelectorChangeHandler(PriceSelector selector)
        {
            this.PriceSelector = selector;
            this.IsSelected = true;

            this.BestSystems = null;

            if (selector is PriceSelector.UserProvided)
            {
                this.BestSystems = null;
                this.Prices = new PricesModel();
            }

            await this.ValidateUserInput();
        }

        protected async Task OnPlanetsChangeHandler(int planetsCount)
        {
            this.MiningPlanets = planetsCount;

            await this.ValidateUserInput();
        }

        protected async Task OnInputPriceChangeHandler(bool success)
        {
            this.BestSystems = null;

            if (success)
            {
                var properties = this.Prices.GetType().GetProperties();

                foreach (var property in properties)
                {
                    var value = this.AppDataService.PlanetaryResourcesPrices[property.Name.ToTitleCase()];
                    property.SetValue(this.Prices, value);
                }

                await this.ValidateUserInput();
            }
        }

        private bool ValidUserPrices()
            => !this.Prices
                .GetType()
                .GetProperties()
                .Select(property => property.GetValue(this.Prices) as decimal? ?? 0)
                .Any(value => value <= 0);

        private async Task ValidateUserInput()
        {
            this.ShowLoader = false;
            this.BestSystems = null;

            if (!this.IsSelected)
            {
                return;
            }

            if (this.MiningPlanets < 1 || this.MiningPlanets > 6)
            {
                return;
            }

            if (this.PriceSelector is PriceSelector.UserProvided)
            {
                if (!this.ValidUserPrices())
                {
                    return;
                }
            }

            this.ShowLoader = true;
            await this.LoadData();
        }
    }
}
