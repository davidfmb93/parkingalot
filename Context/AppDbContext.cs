using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Context
{
    public class AppDbContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Time> Time { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            // Define relationship between Vehicle and Time
            builder.Entity<Time>()
                .HasOne(x => x.Vehicle)
                .WithMany(x => x.Times);

            new DbInitializer(builder).Seed();
        }
    }
}