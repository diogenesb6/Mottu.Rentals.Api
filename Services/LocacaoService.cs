using Mottu.Rentals.Api.Data;
using Mottu.Rentals.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mottu.Rentals.Api.Services
{
    public class RentalService
    {
        private readonly AppDbContext _context;

        public RentalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Rental> CreateRentalAsync(Guid bikeId, Guid riderId, int planDays)
        {
            var bike = await _context.Bikes.FindAsync(bikeId)
                ?? throw new Exception("Bike not found.");

            var rider = await _context.Riders.FindAsync(riderId)
                ?? throw new Exception("Rider not found.");

            if (!rider.LicenseType.Contains("A"))
                throw new Exception("Only riders with category A license can rent.");

            decimal dailyRate = planDays switch
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
                BikeId = bikeId,
                RiderId = riderId,
                PlanDays = planDays,
                DailyRate = dailyRate,
                StartDate = DateTime.UtcNow.Date.AddDays(1),
                ExpectedEndDate = DateTime.UtcNow.Date.AddDays(planDays),
                ExpectedValue = planDays * dailyRate,
                IsActive = true
            };

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return rental;
        }

        public async Task<Rental> FinalizeRentalAsync(Guid rentalId, DateTime returnDate)
        {
            var rental = await _context.Rentals.FindAsync(rentalId)
                ?? throw new Exception("Rental not found.");

            if (!rental.IsActive)
                throw new Exception("Rental already finalized.");

            rental.FinalValue = CalculateFinalValue(rental, returnDate);
            rental.IsActive = false;

            await _context.SaveChangesAsync();
            return rental;
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
