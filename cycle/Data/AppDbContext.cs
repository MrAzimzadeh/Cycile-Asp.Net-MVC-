using cycle.Models;
using Microsoft.EntityFrameworkCore;

namespace cycle.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) { }

        public DbSet<Banner> Banners{ get; set; }
        public DbSet<Prodact> Prodacts { get; set; }

    }
}
