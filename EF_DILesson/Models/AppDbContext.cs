using EF_DILesson.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_DILesson
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<MovieComment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>()
                .HasMany(f => f.Comments)
                .WithOne(c => c.Film)
                .HasForeignKey(c => c.MovieId);
        }
    }
}