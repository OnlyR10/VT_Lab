using Microsoft.EntityFrameworkCore;
using Naydovich.Domain.Entities;

namespace Naydovich.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //public AppDbContext() { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder); optionsBuilder.UseSqlite("");
        //}

        public DbSet<Cleaner> Cleaners { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
