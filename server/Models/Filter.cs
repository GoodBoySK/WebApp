using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace server.Models
{
    public class Filter
    {
        public string? NameFilter { get; set; }
        public string? TagFilter { get; set; }
        public bool? OnlyMy { get; set; }
        [Range(1, int.MaxValue)]
        public int PageSize { get; set; } = 10;
        [Range(0, int.MaxValue)]
        public int Page { get; set; } = 0;
        public OrderBy? Order { get; set; }
        public bool Ascending { get; set; } = true; 
    }
    public enum OrderBy
    {
        [EnumMember(Value = "name")]
        Name,
        [EnumMember(Value = "created_at")]
        CreatedData 
    }
}
