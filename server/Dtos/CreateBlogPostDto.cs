using Microsoft.AspNetCore.Identity;

namespace server.Dtos
{
    public class CreateBlogPostDto
    {
        public string Title { get; set; } = "Title";
        public string Content { get; set; } = "";
        public string Description { get; set; } = "";
        public string? Tag { get; set; }
        public int? MediaFileID { get; set; }
    }
}
