using Microsoft.EntityFrameworkCore;
using Naydovich.Domain.Entities;

namespace Naydovich.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cleaner> Cleaners { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
