using billige_madopskrifter.Model;
using Microsoft.EntityFrameworkCore;


namespace billige_madopskrifter.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }

    }
}
