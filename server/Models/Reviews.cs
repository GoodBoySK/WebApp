using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Reviews
{
    public Guid Id { get; set; }
    public Review[] AllReviews { get; set; }
    [Range(0.0, 5.0)]
    public float Rating { get; set; }
}