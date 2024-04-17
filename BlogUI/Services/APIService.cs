using Blazored.Toast.Services;
using BlogUI.Models;
using Microsoft.AspNetCore.Components;

namespace BlogUI.Services
{
    public class APIService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly NavigationManager _navigationManager;
        private readonly IConfiguration _config;
        private readonly Common _common;
        private readonly IToastService _toastService;
        public APIService(IConfiguration config, IHttpClientFactory clientFactory, NavigationManager navigationManager,Common common,IToastService toastService)
        {
            _toastService = toastService;
            _common = common;
            _clientFactory = clientFactory;
            _navigationManager = navigationManager;
            _config = config;

        }
        public async Task<Stream> SendAsync(string url, bool isAuthenticated = true)
        {
            HttpClient httpClient = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            UserContext context = await _common.GetUserContext();
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.tokenId);

            HttpResponseMessage response = await httpClient.SendAsync(request);
            httpClient.Dispose();
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            else if (response.ReasonPhrase == "Forbidden")
            {
                //_navigationManager.NavigateTo("/unauthorizedpage"); // redirect to unauthorized page
            }
            else if (response.ReasonPhrase == "Unauthorized")
            {
            }
            else
            {
            }
            return null;
        }

        public async Task<Stream> PostAsync(string url, StringContent jsonContent)
        {
            HttpClient httpClient = _clientFactory.CreateClient();
            UserContext userContext = await _common.GetUserContext();

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userContext.tokenId);

            HttpResponseMessage response = await httpClient.PostAsync(url, jsonContent);
            httpClient.Dispose();
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            else if (response.ReasonPhrase == "Forbidden")
            {
                _toastService.ShowWarning("Unauthorized");
            }
            else if (response.ReasonPhrase == "Unauthorized")
            {
                _toastService.ShowWarning("Login to continue");
                _navigationManager.NavigateTo("/Login"); // redirect to login page
            }
            else
            {
                _toastService.ShowWarning(response.ReasonPhrase);
            }
            return null;

        }
    }
}
