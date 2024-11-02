using System.ComponentModel.DataAnnotations;
using server.Models;

namespace server.Dtos;

public class IngredientDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Ingredient ToModel()
    {
        return new Ingredient
        {
            Name = Name,
        };
    }
}