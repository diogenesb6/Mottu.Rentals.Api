using Mottu.Rentals.Api.Data;
using Mottu.Rentals.Api.Entities;
using Mottu.Rentals.Api.DTO;


namespace Mottu.Rentals.Api.Services
{
    using Microsoft.EntityFrameworkCore;

    public class RiderService
    {
        private readonly AppDbContext _context;

        public RiderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Rider>> GetAllAsync()
        {
            return await _context.Riders.ToListAsync();
        }

        public async Task<Rider?> GetByIdAsync(int id)
        {
            return await _context.Riders.FindAsync(id);
        }

        public async Task<Rider> CreateAsync(RiderCreateDto dto)
        {
            var rider = new Rider
            {
                Name = dto.Name,
                Cpf = dto.Cpf,
                BirthDate = dto.BirthDate,
                LicenseNumber = dto.LicenseNumber,
                LicenseType = dto.LicenseType,
                LicenseImage = dto.LicenseImage
            };

            _context.Riders.Add(rider);
            await _context.SaveChangesAsync();
            return rider;
        }

        public async Task<Rider?> UpdateAsync(int id, RiderUpdateDto dto)
        {
            var rider = await _context.Riders.FindAsync(id);
            if (rider == null) return null;

            rider.Name = dto.Name;
            rider.Cpf = dto.Cpf;
            rider.BirthDate = dto.BirthDate;
            rider.LicenseNumber = dto.LicenseNumber;
            rider.LicenseType = dto.LicenseType;
            rider.LicenseImage = dto.LicenseImage;

            _context.Riders.Update(rider);
            await _context.SaveChangesAsync();
            return rider;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var rider = await _context.Riders.FindAsync(id);
            if (rider == null) return false;

            _context.Riders.Remove(rider);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> UploadFotoAsync(IFormFile foto)
        {
            if (foto == null || foto.Length == 0)
                return string.Empty;

            var ext = Path.GetExtension(foto.FileName).ToLower();
            if (ext != ".png" && ext != ".bmp")
                throw new Exception("Formato inválido");

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine("Storage/FotosCNH", fileName);

            Directory.CreateDirectory("Storage/FotosCNH");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            return fileName;
        }
    }


}
