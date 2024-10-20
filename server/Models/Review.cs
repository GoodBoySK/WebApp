using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Review : Comment
{
    public Review()
    {
        
    }
    public Review(User createdBy) : base(createdBy)
    {
    }

    [Range(0.0,5.0)]
    [Required]
    public float Rating { get; set; }
}