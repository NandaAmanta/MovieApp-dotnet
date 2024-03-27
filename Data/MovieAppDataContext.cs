// using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using MovieApp.Models;

namespace MovieApp.Data;

public class MovieAppDataContext : DbContext
{
    public MovieAppDataContext(DbContextOptions<MovieAppDataContext> options) : base(options)
    {
    }

    /**
    * Auto-update create_at and updated_at columns
    */
    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.Now;
            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
            }
        }
        return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Movie>()
        //     .HasMany(m => m.MovieSchedules)
        //     .WithOne(ms => ms.Movie)
        //     .HasForeignKey(ms => ms.MovieId);

        // modelBuilder.Entity<Studio>()
        //     .HasMany(s => s.MovieSchedules)
        //     .WithOne(ms => ms.Studio)
        //     .HasForeignKey(ms => ms.StudioId);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> User { get; set; }
    public DbSet<Movie> Movie { get; set; }
    public DbSet<MovieSchedule> MovieSchedule { get; set; }
    public DbSet<Tag> Tag { get; set; }
    public DbSet<MovieTag> MovieTag { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderItem> OrderItem { get; set; }
    public DbSet<Studio> Studio { get; set; }
}
