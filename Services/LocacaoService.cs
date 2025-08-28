using Microsoft.EntityFrameworkCore;
using Mottu.Rentals.Api.Data;
using Mottu.Rentals.Api.DTO;
using Mottu.Rentals.Api.Entities;

namespace Mottu.Rentals.Api.Services
{
    public class RentalService
    {
        private readonly AppDbContext _context;

        public RentalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Rental> CreateRentalAsync(RentalCreateDto dto)
        {
            var bike = await _context.Bikes.FindAsync(dto.BikeId)
                ?? throw new Exception("Bike not found.");

            var rider = await _context.Riders.FindAsync(dto.RiderId)
                ?? throw new Exception("Rider not found.");

            if (!rider.LicenseType.Contains("A"))
                throw new Exception("Only riders with category A license can rent.");

            decimal dailyRate = dto.PlanDays switch
            {
                7 => 30m,
                15 => 28m,
                30 => 22m,
                45 => 20m,
                50 => 18m,
                _ => throw new Exception("Invalid plan.")
            };

            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                BikeId = dto.BikeId,
                RiderId = dto.RiderId,
                PlanDays = dto.PlanDays,
                DailyRate = dailyRate,
                StartDate = DateTime.UtcNow.Date.AddDays(1),
                ExpectedEndDate = DateTime.UtcNow.Date.AddDays(dto.PlanDays),
                ExpectedValue = dto.PlanDays * dailyRate,
                IsActive = true
            };

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return rental;
        }

        public async Task<Rental> FinalizeRentalAsync(Guid rentalId, RentalReturnDto dto)
        {
            var rental = await _context.Rentals.FindAsync(rentalId)
                ?? throw new Exception("Rental not found.");

            if (!rental.IsActive)
                throw new Exception("Rental already finalized.");

            rental.EndDate = dto.ReturnDate;
            rental.FinalValue = CalculateFinalValue(rental, dto.ReturnDate);
            rental.IsActive = false;

            await _context.SaveChangesAsync();
            return rental;
        }

        public async Task<Rental?> GetRentalByIdAsync(Guid rentalId)
        {
            return await _context.Rentals
                .Include(r => r.Bike)
                .Include(r => r.Rider)
                .FirstOrDefaultAsync(r => r.Id == rentalId);
        }

        public async Task<List<Rental>> ListRentalsAsync()
        {
            return await _context.Rentals
                .Include(r => r.Bike)
                .Include(r => r.Rider)
                .ToListAsync();
        }

        public async Task<bool> CancelRentalAsync(Guid rentalId)
        {
            var rental = await _context.Rentals.FindAsync(rentalId);
            if (rental == null || !rental.IsActive) return false;

            rental.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        private decimal CalculateFinalValue(Rental rental, DateTime returnDate)
        {
            if (returnDate < rental.ExpectedEndDate)
            {
                int daysUsed = Math.Max((returnDate - rental.StartDate).Days, 1);
                int unusedDays = rental.PlanDays - daysUsed;
                decimal baseValue = daysUsed * rental.DailyRate;
                decimal penalty = rental.PlanDays switch
                {
                    7 => unusedDays * rental.DailyRate * 0.20m,
                    15 => unusedDays * rental.DailyRate * 0.40m,
                    _ => 0m
                };
                return baseValue + penalty;
            }

            if (returnDate > rental.ExpectedEndDate)
            {
                int lateDays = (returnDate - rental.ExpectedEndDate).Days;
                return rental.ExpectedValue + (lateDays * 50m);
            }

            return rental.ExpectedValue;
        }
    }


}
