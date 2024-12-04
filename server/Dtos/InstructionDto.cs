using server.Models;
using System.ComponentModel.DataAnnotations;

namespace server.Dtos
{
    public class InstructionDto
    {
        public int Position { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public MediaFileDto Media { get; set; } = new MediaFileDto();
    }
}
