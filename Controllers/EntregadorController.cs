using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mottu.Rentals.Api.Data;
using Mottu.Rentals.Api.DTO;
using Mottu.Rentals.Api.Entities;

namespace Mottu.Rentals.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntregadorController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEntregadorService _service;

        public EntregadorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entregadores = await _service.GetAllAsync();
            return Ok(entregadores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entregador = await _service.GetByIdAsync(id);
            if (entregador == null) return NotFound();
            return Ok(entregador);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EntregadorCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] EntregadorUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        public async Task<string> UploadFotoAsync(IFormFile foto)
        {
            if (foto == null || foto.Length == 0)
                return null;

            var ext = Path.GetExtension(foto.FileName).ToLower();
            if (ext != ".png" && ext != ".bmp")
                throw new Exception("Formato inválido");

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine("Storage/FotosCNH", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            return fileName; // ou URL se estiver S3/MinIO
        }
    }
}
