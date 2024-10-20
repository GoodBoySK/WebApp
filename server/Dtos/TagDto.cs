using server.Models;

namespace server.Dtos
{
    public class TagDto
    {
        public string Name { get; set; } = string.Empty;

        public Tag ToModel()
        {
            return new Tag(Name);
        }
    }
}
