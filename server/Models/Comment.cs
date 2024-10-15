using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Comment(User createdBy)
{
    public Guid Id { get; set; }
    [Required]
    public User CreatedBy { get; set; } = createdBy;
    [StringLength(500)]
    public string Text { get; set; } = string.Empty;
    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Comment? Parent { get; set; }
}