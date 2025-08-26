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

            // Configurações de unicidade
            modelBuilder.Entity<Moto>()
                .HasIndex(m => m.Placa)
                .IsUnique();

            modelBuilder.Entity<Entregador>()
                .HasIndex(e => e.CNPJ)
                .IsUnique();

            modelBuilder.Entity<Entregador>()
                .HasIndex(e => e.NumeroCNH)
                .IsUnique();

            // Relacionamentos
            modelBuilder.Entity<Locacao>()
                .HasOne<Moto>()
                .WithMany()
                .HasForeignKey(l => l.MotoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Locacao>()
                .HasOne<Entregador>()
                .WithMany()
                .HasForeignKey(l => l.EntregadorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
