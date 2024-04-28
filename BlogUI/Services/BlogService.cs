using Microsoft.JSInterop;
using System.Text.Json;
using BlogUI.Models;
using BlogUI.Extensions;
using System.Text;

namespace BlogUI.Services
{
    public class BlogService
    {
        private readonly APIService apiService;
        private readonly IConfiguration configuration;
        private readonly IJSRuntime JS;
        public BlogService(APIService apiService, IConfiguration configuration, IJSRuntime jS)
        {
            this.apiService = apiService;
            this.configuration = configuration;
            JS = jS;
        }

        public async Task<IEnumerable<Blog>> GetBlogListForDashboard()
        {
            var requestUrl = configuration["APIBaseUrl"] + "Blog/GetBlogsListForDashboard";
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

        public async Task<IEnumerable<Blog>> GetBlogListForBlogs()
        {
            var requestUrl = configuration["APIBaseUrl"] + "Blog/GetBlogsListForBlogs";
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

        public async Task<IEnumerable<Notifications>> GetNotificationsForUser(int userId)
        {
            var requestUrl = configuration["APIBaseUrl"] + "Blog/GetUserNotifications/" + userId;
            var responseStream = await apiService.SendAsync(requestUrl, true);
            if (responseStream != null)
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<Notifications>>(responseStream);
            }
            else
            {
                return null;
            }
        }

        public async Task<Blog> GetBlogDetail(int? id)
        {
            var requestUrl = configuration["APIBaseUrl"] + $"Blog/GetBlogDetail/{id}" ;
            var responseStream = await apiService.SendAsync(requestUrl, true);
            if (responseStream != null)
            {
                return await JsonSerializer.DeserializeAsync<Blog>(responseStream);
            }
            else
            {
                return null;
            }
        }

        public async void DeleteBlog(Blog blog)
        {
            if (blog.blogId > 0)
            {
                bool confirmResult = await JS.InvokeAsync<bool>("confirm", "Are you sure you delete the Holiday?");
                if (confirmResult)
                {
                    var requestUrl = configuration["APIBaseUrl"] + "Blog/DeleteBlog/" + blog.blogId;
                    bool isSuccess = await apiService.DeleteAsync(requestUrl);
                    if (isSuccess)
                    {
                        JS.ToastrSuccess("Blog Deleted Successfully !!!");
                    }
                    else
                    {
                        JS.ToastrError("Something Went Wrong !!!");
                    }

                }
            }
        }

        public async Task<ResponseModel> AddEditBlog(Blog blog)
        {
            var requestUrl = configuration["APIBaseUrl"] + "blog/editBlog";

            if (blog.blogId == 0)
            {
                requestUrl = configuration["APIBaseUrl"] + "blog/addblog";
            }
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    blogId = blog.blogId,
                    blogTitle = blog.blogTitle,
                    blogDescription = blog.blogDescription,
                    userId = blog.userId,
                    blogImages = blog.blogImages,
                }),
                Encoding.UTF8,
                "application/json");

            using var responseStream = await apiService.PostAsync(requestUrl, jsonContent);

            if (responseStream.Length > 0)
            {
                ResponseModel responseModel = await JsonSerializer.DeserializeAsync<ResponseModel>(responseStream);
                return responseModel;
            }
            else
            {
                JS.ToastrSuccess("Something went wrong !!!");
                return null;
            }

        }
    }
}
