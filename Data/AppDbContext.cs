using Microsoft.EntityFrameworkCore;
using Mottu.Rentals.Api.Entities;

namespace Mottu.Rentals.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Moto> Motos { get; set; } = null!;
        public DbSet<Entregador> Entregadores { get; set; } = null!;
        public DbSet<Locacao> Locacoes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da tabela de motos
            modelBuilder.Entity<Moto>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Year)
                      .IsRequired();

                entity.Property(m => m.Model)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(m => m.Plate)
                      .IsRequired()
                      .HasMaxLength(10);

                // Placa é única
                entity.HasIndex(m => m.Plate).IsUnique();
            });
        }
    }
}
