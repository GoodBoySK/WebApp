namespace server.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public required User Autor { get; set; }
        public required Tag? Tag { get; set; }
        public string Title { get; set; } = "Title";
        public string Content { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public string Desctiption { get; set; } = "";
        public MediaFile? Thumbnail { get; set; }
    }
}
