using Microsoft.EntityFrameworkCore;
using SuperheroApp.Core.Entities;

namespace SuperheroApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Hero> Heroes { get; set; } = null!;
        public DbSet<Superpower> Superpowers { get; set; } = null!;
        public DbSet<HeroSuperpower> HeroSuperpowers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hero>(entity => 
            {
                entity.Property(h => h.Name).IsRequired().HasMaxLength(100);
                entity.HasIndex(h => h.Name).IsUnique();
                entity.Property(h => h.HeroName).IsRequired().HasMaxLength(100);
                entity.Property(h => h.Height).HasColumnType("decimal(5,2)");
                entity.Property(h => h.Weight).HasColumnType("decimal(5,2)");
            });

            modelBuilder.Entity<Superpower>(entity => 
            {
                entity.Property(s => s.Name).IsRequired().HasMaxLength(50);
                entity.Property(s => s.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<HeroSuperpower>(entity => 
            {
                entity.HasKey(hs => new { hs.HeroId, hs.SuperpowerId });
                
                entity.HasOne(hs => hs.Hero)
                    .WithMany(h => h.HeroSuperpowers)
                    .HasForeignKey(hs => hs.HeroId);
                
                entity.HasOne(hs => hs.Superpower)
                    .WithMany(s => s.HeroSuperpowers)
                    .HasForeignKey(hs => hs.SuperpowerId);
            });
        }
    }
}