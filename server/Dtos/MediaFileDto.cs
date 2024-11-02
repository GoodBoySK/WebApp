using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace server.Dtos;

public class MediaFileDto
{
    [Required]
    public int Id { get; set; }
    public IFormFile? Image { get; set; }
}