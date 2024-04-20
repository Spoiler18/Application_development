using BlogUI.Extensions;
using BlogUI.Models;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Text;

namespace BlogUI.Services
{
    public class ReactionCommentService
    {
        private readonly APIService apiService;
        private readonly IConfiguration configuration;
        private readonly IJSRuntime JS;
        public ReactionCommentService(APIService apiService, IConfiguration configuration, IJSRuntime jS)
        {
            this.apiService = apiService;
            this.configuration = configuration;
            JS = jS;
        }

        public async Task<ResponseModel> AddEditBlogReaction(int reactionId,int userReaction, int userOldReaction, int userId, int blogId)
        {
            var requestUrl = configuration["APIBaseUrl"] + "ReactionComment/EditBlogReaction";

            if (reactionId == 0)
            {
                requestUrl = configuration["APIBaseUrl"] + "ReactionComment/AddBlogReaction";
            }
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    ReactionId = reactionId,
                    UserReaction = userReaction,
                    BlogId = blogId,
                    UserId = userId,
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

        public async Task<ResponseModel> AddEditBlogCommentReaction(int commentReactionId, int userReaction, int commentId, int userId)
        {
            var requestUrl = configuration["APIBaseUrl"] + "ReactionComment/EditBlogCommentReaction";

            if (commentReactionId == 0)
            {
                requestUrl = configuration["APIBaseUrl"] + "ReactionComment/AddBlogCommentReaction";
            }
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    CommentReactionId = commentReactionId,
                    CommentId = commentId,
                    UserReaction = userReaction,
                    UserId = userId,
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

        public async Task<ResponseModel> AddEditBlogComment(int commentId, string? userComment, int userId, int blogId, int replyToCommentId)
        {
            var requestUrl = configuration["APIBaseUrl"] + "ReactionComment/EditBlogComment";

            if (commentId == 0)
            {
                requestUrl = configuration["APIBaseUrl"] + "ReactionComment/AddBlogComment";
            }
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    CommentId = commentId,
                    UserComment = userComment,
                    BlogId = blogId,
                    UserId = userId,
                    ReplyToCommentId = replyToCommentId,
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

        public async void DeleteBlogReaction(int reactionId)
        {
            if (reactionId > 0)
            {
                var requestUrl = configuration["APIBaseUrl"] + "ReactionComment/DeleteBlogReaction/" + reactionId;
                bool isSuccess = await apiService.DeleteAsync(requestUrl);
                if (isSuccess)
                {
                }
                else
                {
                    JS.ToastrError("Something Went Wrong !!!");
                }
            }
        }

        public async void DeleteBlogComment(int commentId)
        {
            if (commentId > 0)
            {
                var requestUrl = configuration["APIBaseUrl"] + "ReactionComment/DeleteBlogComment/" + commentId;
                bool isSuccess = await apiService.DeleteAsync(requestUrl);
                if (isSuccess)
                {
                }
                else
                {
                    JS.ToastrError("Something Went Wrong !!!");
                }
            }
        }

        public async void DeleteBlogCommentReaction(int commentReactionId)
        {
            if (commentReactionId > 0)
            {
                var requestUrl = configuration["APIBaseUrl"] + "ReactionComment/DeleteBlogCommentReaction/" + commentReactionId;
                bool isSuccess = await apiService.DeleteAsync(requestUrl);
                if (isSuccess)
                {
                }
                else
                {
                    JS.ToastrError("Something Went Wrong !!!");
                }
            }
        }
    }
}
