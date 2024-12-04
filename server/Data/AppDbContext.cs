using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<MediaFile> MediaFiles { get; set; }
    public DbSet<Instruction> Instructions { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Reviews> Reviews { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<DishType> DishTypes { get; set; }

}