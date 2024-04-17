namespace BlogUI.Models
{
    public class Blog
    {
        public int blogId { get; set; }
        public int? userId { get; set; }
        public string? blogTitle { get; set; }
        public string? blogDescription { get; set; }
    }
}
