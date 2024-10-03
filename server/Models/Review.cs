using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Review : Comment
    {
        public int Id { get; set; }
        [Range(0.0,5.0)]
        [Required]
        public float Rating { get; set; }
    }
}
