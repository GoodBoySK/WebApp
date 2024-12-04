using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Instruction
{
    public int Id { get; set; }
    public int Position { get; set; } = 0;
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    public required MediaFile Media { get; set; }

}