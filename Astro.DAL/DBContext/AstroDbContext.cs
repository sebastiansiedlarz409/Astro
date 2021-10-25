using Astro.DAL.Configuration;
using Astro.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Astro.DAL.DBContext
{
    public class AstroDbContext : IdentityDbContext<User>
    {
        public AstroDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<APOD> APOD { get; set; }
        public DbSet<EPIC> EPIC { get; set; }
        public DbSet<Insight> Insights { get; set; }
        public DbSet<AsteroidsNeoWs> AsteroidsNeoWs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Topic>()
                .HasOne(p => p.User)
                .WithMany(b => b.Topics);

            modelBuilder.Entity<Comment>()
                .HasOne(p => p.Topic)
                .WithMany(b => b.Comments);

            modelBuilder.Entity<Comment>()
                .HasOne(p => p.User)
                .WithMany(p => p.Comments);
        }
    }
}
