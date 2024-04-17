using BlogUI.Models;
using Blazored.LocalStorage;

namespace BlogUI.Services
{
    public class Common
    {
        private readonly ILocalStorageService _localStorageService;
        public Common(ILocalStorageService localStorageService) 
        {
            _localStorageService = localStorageService;
        }

        public async Task<UserContext> GetUserContext()
        {
            return await _localStorageService.GetItemAsync<UserContext>("userContext") ?? new UserContext();
        }
    }
}
