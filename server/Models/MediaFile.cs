namespace server.Models
{
    public class MediaFile
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
