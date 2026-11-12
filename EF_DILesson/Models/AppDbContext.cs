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
    }
}