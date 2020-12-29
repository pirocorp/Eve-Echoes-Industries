namespace EveEchoesPlanetaryProductionApi.Web.Helpers
{
    using System;
    using System.Net;

    using EveEchoesPlanetaryProductionApi.Web.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Rendering;

    public class AppRouteView : RouteView
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IAppDataService AppDataService { get; set; }

        protected override void Render(RenderTreeBuilder builder)
        {
            var authorize = Attribute.GetCustomAttribute(this.RouteData.PageType, typeof(AuthorizeAttribute)) != null;

            if (authorize && this.AppDataService.User is null)
            {
                var returnUrl = WebUtility.UrlEncode(new Uri(this.NavigationManager.Uri).PathAndQuery);
                this.NavigationManager.NavigateTo($"account/login?returnUrl={returnUrl}");
            }
            else
            {
                base.Render(builder);
            }
        }
    }
}
