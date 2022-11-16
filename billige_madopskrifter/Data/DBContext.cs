using billige_madopskrifter.Model;
using Microsoft.EntityFrameworkCore;


namespace billige_madopskrifter.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Review> Reviews { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Recipe>().HasMany(r => r.Ingredients).WithOne(i => i.Recipe);
        //    modelBuilder.Entity<Ingredient>().HasOne(i => i.Recipe).WithMany(r => r.Ingredients);
        //}

    }
}
