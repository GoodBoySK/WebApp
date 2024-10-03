namespace server.Models
{
    public class Instruction
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public string Description { get; set; }
        public MediaFile? Media { get; set; }

    }
}
