using Lab5.Data.Entities;
using System.Data.Entity;

namespace Lab5.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new AppDbInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Pet> Pets { get; set; }
    }

    public class AppDbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        // intentionally left blank
    }
}