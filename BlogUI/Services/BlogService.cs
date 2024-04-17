using Microsoft.JSInterop;
using System.Text.Json;
using BlogUI.Models;

namespace BlogUI.Services
{
    public class BlogService
    {
        private readonly APIService apiService;
        private readonly IConfiguration configuration;

        public BlogService(APIService apiService, IConfiguration configuration)
        {
            this.apiService = apiService;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<Blog>> GetWeatherDetails()
        {
            var requestUrl = configuration["APIBaseUrl"] + "Blog/GetBlogsList";
            var responseStream = await apiService.SendAsync(requestUrl, true);
            if (responseStream != null)
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<Blog>?>(responseStream);
            }
            else
            {
                return null;
            }

        }
    }
}
