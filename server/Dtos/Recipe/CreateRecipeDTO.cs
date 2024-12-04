using System.ComponentModel.DataAnnotations;

namespace server.Dtos;

public class CreateRecipeDto
{
    [Required]
    public required string Name { get; set; } 
}