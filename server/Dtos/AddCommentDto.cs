namespace server.Dtos
{
    public class AddCommentDto
    {
        public string Text { get; set; } = string.Empty;
        public Guid? ParentId { get; set; }
    }
}
