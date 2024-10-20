using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Tag
{
    public Tag()
    {
        
    }
    public Tag(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
}