using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mottu.Rentals.Api.Data;
using Mottu.Rentals.Api.Entities;

namespace Mottu.Rentals.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MotoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMoto([FromBody] Moto moto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verifica se já existe moto com mesma placa
            var exists = _context.Motos.Any(m => m.Plate == moto.Plate);
            if (exists)
                return Conflict(new { message = "Já existe uma moto com essa placa." });

            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMotoById), new { id = moto.Id }, moto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMotoById(string? plate)
        {
            var motos = _context.Motos.AsQueryable();

            if (!string.IsNullOrEmpty(plate))
            {
                motos = motos.Where(m => m.Plate.Contains(plate));
            }

            return Ok(motos.ToList());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Moto moto)
        {
            if (id != moto.Id) return BadRequest();
            _context.Entry(moto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null) return NotFound();
            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
