using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Reviews
{
    public Guid Id { get; set; }
    public List<Review> AllReviews { get; set; } = new List<Review>();
    [Range(0.0, 5.0)] 
    public float Rating { get; set; } = 0.0f;
}