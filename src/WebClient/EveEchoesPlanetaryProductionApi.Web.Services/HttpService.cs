namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.Auth;
    using EveEchoesPlanetaryProductionApi.Web.Common;
    using EveEchoesPlanetaryProductionApi.Web.Services.Helpers;

    using Microsoft.AspNetCore.Components;

    public class HttpService : IHttpService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;
        private readonly ILocalStorageService localStorageService;

        public HttpService(
            HttpClient httpClient,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
            this.localStorageService = localStorageService;
        }

        public async Task<T> Get<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await this.SendRequest<T>(request);
        }

        public async Task Post(string uri, object value)
        {
            var request = CreateRequest(HttpMethod.Post, uri, value);
            await this.SendRequest(request);
        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var request = CreateRequest(HttpMethod.Post, uri, value);
            return await this.SendRequest<T>(request);
        }

        public async Task Put(string uri, object value)
        {
            var request = CreateRequest(HttpMethod.Put, uri, value);
            await this.SendRequest(request);
        }

        public async Task<T> Put<T>(string uri, object value)
        {
            var request = CreateRequest(HttpMethod.Put, uri, value);
            return await this.SendRequest<T>(request);
        }

        public async Task Delete(string uri)
        {
            var request = CreateRequest(HttpMethod.Delete, uri);
            await this.SendRequest(request);
        }

        public async Task<T> Delete<T>(string uri)
        {
            var request = CreateRequest(HttpMethod.Delete, uri);
            return await this.SendRequest<T>(request);
        }

        private static async Task HandleErrors(HttpResponseMessage response)
        {
            // throw exception on error response
            if (!response.IsSuccessStatusCode)
            {
                IEnumerable<ApiErrorModel> errors;

                try
                {
                    errors = await response.Content.ReadFromJsonAsync<IEnumerable<ApiErrorModel>>();
                }
                catch (Exception)
                {
                    errors = new[]
                    {
                        new ApiErrorModel
                        {
                            Code = string.Empty,
                            Description = "Something went wrong try again later.",
                        },
                    };
                }

                throw new Exception(errors?.First().Description);
            }
        }

        private static HttpRequestMessage CreateRequest(HttpMethod method, string uri, object value = null)
        {
            var request = new HttpRequestMessage(method, uri);

            if (value != null)
            {
                request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            }

            return request;
        }

        private async Task SendRequest(HttpRequestMessage request)
        {
            await this.AddJwtHeader(request);

            // send request
            using var response = await this.httpClient.SendAsync(request);

            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                this.navigationManager.NavigateTo("account/logout");
                return;
            }

            await HandleErrors(response);
        }

        private async Task<T> SendRequest<T>(HttpRequestMessage request)
        {
            await this.AddJwtHeader(request);

            // send request
            using var response = await this.httpClient.SendAsync(request);

            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                this.navigationManager.NavigateTo("account/logout");
                return default;
            }

            await HandleErrors(response);

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            options.Converters.Add(new StringConverter());

            return await response.Content.ReadFromJsonAsync<T>(options);
        }

        private async Task AddJwtHeader(HttpRequestMessage request)
        {
            // add jwt auth header if it's present
            var user = await this.localStorageService.GetItem<UserResponseModel>(PresentationConstants.UserKey);

            if (user is not null && !string.IsNullOrWhiteSpace(user.Token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
            }
        }
    }
}
