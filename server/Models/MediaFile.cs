using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class MediaFile(string path)
{
    public int Id { get; set; }
    [StringLength(100)]
    public string Path { get; set; } = path;
    public User? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
}