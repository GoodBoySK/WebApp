using server.Models;

namespace server.Dtos
{
    public class CommentDto
    {
        public DateTime CreatedAt { get; set; }

        public string Text { get; set; } = string.Empty;

        public Comment? Parent { get; set; }

    }
}
