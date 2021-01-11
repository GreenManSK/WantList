using Microsoft.EntityFrameworkCore;
using WantList.Core;

namespace WantList.Data
{
    public class WantListDbContext : DbContext
    {
        public WantListDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Anime> Animes { get; set; }
        public DbSet<AnidbAnime> AnidbAnimes { get; set; }
        public DbSet<Settings> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Anime>().HasIndex(a => a.AnidbId).IsUnique();
        }
    }
}