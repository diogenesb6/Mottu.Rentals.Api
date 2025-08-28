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

        public DbSet<Bike> Bikes { get; set; } = null!;
        public DbSet<Rider> Riders { get; set; } = null!;
        public DbSet<Rental> Rentals { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da tabela de bikes
            modelBuilder.Entity<Bike>(entity =>
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

                entity.HasIndex(m => m.Plate).IsUnique();
            });

            // Configuração da tabela de riders
            modelBuilder.Entity<Rider>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(r => r.Cpf)
                      .IsRequired()
                      .HasMaxLength(14);

                entity.Property(r => r.LicenseNumber)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(r => r.LicenseType)
                      .IsRequired()
                      .HasMaxLength(2);

                entity.HasIndex(r => r.Cpf).IsUnique();
                entity.HasIndex(r => r.LicenseNumber).IsUnique();
            });

            // Configuração da tabela de rentals
            modelBuilder.Entity<Rental>(entity =>
            {
                entity.HasKey(l => l.Id);

                entity.Property(l => l.PlanDays).IsRequired();
                entity.Property(l => l.DailyRate).IsRequired();
                entity.Property(l => l.ExpectedValue).IsRequired();
                entity.Property(l => l.FinalValue);
                entity.Property(l => l.StartDate).IsRequired();
                entity.Property(l => l.ExpectedEndDate).IsRequired();
                entity.Property(l => l.EndDate);
                entity.Property(l => l.IsActive).IsRequired();

                entity.HasOne(l => l.Bike)
                      .WithMany()
                      .HasForeignKey(l => l.BikeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(l => l.Rider)
                      .WithMany()
                      .HasForeignKey(l => l.RiderId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
