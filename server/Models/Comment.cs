using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Comment
{
    public Guid Id { get; set; }
    [Required]
    public User CreatedBy { get; set; }
    public string Text { get; set; } = string.Empty;
    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Comment? Parent { get; set; }
}