using server.Models;

namespace server.Dtos
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Text { get; set; } = string.Empty;

        public Comment? Parent { get; set; }
        public User CreatedBy { get; set; }

    }
}
