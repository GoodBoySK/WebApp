using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Tag
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    public Tag()
    {

    }
    public Tag(string name)
    {
        Name = name;
    }

}