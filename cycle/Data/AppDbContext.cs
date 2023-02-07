using cycle.Models;
using Microsoft.EntityFrameworkCore;

namespace cycle.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) { }

        public DbSet<Banner> Banners{ get; set; }
        public DbSet<Prodact> Prodacts { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Testomonia> Testomonias{ get; set; }

    }
}
