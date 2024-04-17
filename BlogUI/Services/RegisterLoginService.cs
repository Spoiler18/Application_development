using BlogUI.Models;
using System.Text.Json;
using System.Text;
using Blazored.LocalStorage;

namespace BlogUI.Services
{
    public class RegisterLoginService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IConfiguration _configuration;
        public RegisterLoginService(IConfiguration configuration, ILocalStorageService localStorageService) 
        {
            _localStorageService = localStorageService;
            _configuration = configuration;
        }

        public async Task<bool> UserLogin(LoginDetail cred)
        {
            HttpClient httpClient = new HttpClient();
            var requestUrl = _configuration["APIBaseUrl"] + "account/login";

            using StringContent jsonContent = new(
            JsonSerializer.Serialize(new
            {
                Email = cred.Email.Trim(),
                Password = @cred.Password.Trim()
            }),
            Encoding.UTF8,
            "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(requestUrl, jsonContent);

            httpClient.Dispose();
            using var responseStream = await response.Content.ReadAsStreamAsync();

            UserContext context = await JsonSerializer.DeserializeAsync<UserContext>(responseStream);

            if (!string.IsNullOrEmpty(context.tokenId))
            {
                await _localStorageService.SetItemAsync("userContext", context);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
