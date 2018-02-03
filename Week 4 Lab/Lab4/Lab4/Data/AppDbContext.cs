using Lab4.Data.Entities;
using System.Data.Entity;

namespace Lab4.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
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