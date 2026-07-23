using Microsoft.EntityFrameworkCore;
using FikirHavuzu.Entities;

namespace FikirHavuzu.Repositories
{
    public class FikirHavuzuDbContext : DbContext
    {
        public FikirHavuzuDbContext(DbContextOptions<FikirHavuzuDbContext> options)
            : base(options)
        {
        }

        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Fikir> Fikirler { get; set; }
        public DbSet<Yetki> Yetkiler { get; set; }
        public DbSet<KullaniciYetki> KullaniciYetkileri { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KullaniciYetki>()
                .HasKey(ky => new { ky.KullaniciId, ky.YetkiId });

            modelBuilder.Entity<KullaniciYetki>()
                .HasOne(ky => ky.Kullanici)
                .WithMany(k => k.KullaniciYetkileri)
                .HasForeignKey(ky => ky.KullaniciId);

            modelBuilder.Entity<KullaniciYetki>()
                .HasOne(ky => ky.Yetki)
                .WithMany(y => y.KullaniciYetkileri)
                .HasForeignKey(ky => ky.YetkiId);
            modelBuilder.Entity<Yetki>().HasData(
                new Yetki { Id = 1, Ad = "Admin" },
                new Yetki { Id = 2, Ad = "Degerlendirici" },
                new Yetki { Id = 3, Ad = "Kullanici" }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}