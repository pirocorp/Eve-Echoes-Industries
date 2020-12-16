namespace EveEchoesPlanetaryProductionApi.Web.Components
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components;

    public abstract class PaginationBase : ComponentBase
    {
        [Parameter] 
        public int PageNumber { get; set; } = 1;

        protected int TotalPages { get; set; }

        protected NavigationManager NavigationManager { get; set; }

        protected string Location { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine(this.PageNumber);

            if (this.PageNumber <= 0)
            {
                this.PageNumber = 1;

                await this.LoadData();

                this.NavigationManager.NavigateTo($"/{this.Location}/1");
            }

            await this.LoadData();
        }

        protected abstract Task LoadData();

        protected async Task ChangePage(int page)
        {
            this.PageNumber = page;
            await this.LoadData();

            if (this.Location is not null)
            {
                this.NavigationManager.NavigateTo($"/{this.Location}/{page}");
            }
        }
    }
}
