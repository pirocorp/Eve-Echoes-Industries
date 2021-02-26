namespace EveEchoesPlanetaryProductionApi.Web.Helpers
{
    using System;
    using System.Collections.Specialized;
    using System.Web;

    using Microsoft.AspNetCore.Components;

    public static class NavigationManagerExtensions
    {
        public static NameValueCollection QueryString(this NavigationManager navigationManager)
        {
            return HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);
        }

        public static string QueryString(this NavigationManager navigationManager, string key)
        {
            return navigationManager.QueryString()[key];
        }
    }
}
