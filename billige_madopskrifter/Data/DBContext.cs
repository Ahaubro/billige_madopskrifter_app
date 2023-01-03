using billige_madopskrifter.Model;
using Microsoft.EntityFrameworkCore;


namespace billige_madopskrifter.Data
{
    // Lavet med hjælp fra projektet https://github.com/Ahaubro/Wemuda-book-app 
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<LikedRecipes> LikedRecipes { get; set; }
        public DbSet<Allergies> Allergies { get; set; }

    }
}
