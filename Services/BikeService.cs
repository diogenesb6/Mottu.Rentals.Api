using Mottu.Rentals.Api.Data;
using Mottu.Rentals.Api.DTO;
using Mottu.Rentals.Api.Entities;

namespace Mottu.Rentals.Api.Services
{
    public class BikeService
    {
        private readonly AppDbContext _context;

        public BikeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Bike> CreateBikeAsync(BikeCreateDto dto)
        {
            var bike = new Bike
            {
                Id = Guid.NewGuid(),
                Model = dto.Model,
                Year = dto.Year,
                Plate = dto.Plate
            };

            _context.Bikes.Add(bike);
            await _context.SaveChangesAsync();
            return bike;
        }

        public async Task<Bike?> GetBikeByIdAsync(Guid id)
        {
            return await _context.Bikes.FindAsync(id);
        }

        public async Task<List<Bike>> ListBikesAsync()
        {
            //retorno async
            return _context.Bikes.ToList();
        }

        public async Task<bool> DeleteBikeAsync(Guid id)
        {
            var bike = await _context.Bikes.FindAsync(id);
            if (bike == null) return false;

            _context.Bikes.Remove(bike);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
