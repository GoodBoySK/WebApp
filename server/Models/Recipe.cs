namespace server.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "Recipe";
        public string Description { get; set; } = string.Empty;
        public DishType DishType { get; set; }
        public MediaFile? SpotPicture { get; set; }
        public Ingredient[] Ingredients { get; set; }
        public Instruction[] Instructions { get; set; }
        public User Author { get; set; }
        public Reviews Reviews { get; set; }
        public Comment[] Comments { get; set; }
        public Tag[] Tags { get; set; }
    }
}
