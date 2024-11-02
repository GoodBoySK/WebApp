using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Ingredient
{
    public Guid Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    public int Amount { get; set; } = 1;
}