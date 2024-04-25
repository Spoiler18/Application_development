using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogUI.Models
{
    public class Blog
    {
        public int blogId { get; set; }
        public int? userId { get; set; }
        public int? popularity { get; set; }
        public string? blogTitle { get; set; }
        public string? blogDescription { get; set; }
        public string? fullName { get; set; }
        public bool? isDeleted { get; set; }
        public DateTime? createdOn { get; set; }
        public List<byte[]>? imageBytesList { get; set; }
        public IEnumerable<BlogImage>? blogImages { get; set; }
        public IEnumerable<DetailedReaction>? blogReactions { get; set; }
        public IEnumerable<DetailedComment>? blogComments { get; set; }
    }

    public class BlogImage 
    {
        public int imageId { get; set; }
        public int? blogId { get; set; }
        public string? imagePath { get; set; }
        public byte[]? imageBytes { get; set; }
    }

    public class DetailedReaction
    {
        public int reactionId { get; set; }
        public int? userId { get; set; }
        public int? blogId { get; set; }
        public int? userReaction { get; set; }
        public string? userReactionFullName { get; set; }
    }

    public class DetailedComment
    {
        public int commentId { get; set; }
        public int? userId { get; set; }
        public int? blogId { get; set; }
        public string? userComment { get; set; }
        public bool? isDeleted { get; set; }
        public int? replyToCommentId { get; set; }
        public string? userCommentFullName { get; set; }
        public IEnumerable<DetailedCommentReaction> commentReactions { get; set; }
    }

    public class DetailedCommentReaction
    {
        public int commentReactionId { get; set; }
        public int? commentId { get; set; }
        public int? userId { get; set; }
        public int? userReaction { get; set; }
        public string? userCommentReactionFullName { get; set; }
    }
}
