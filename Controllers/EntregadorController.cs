using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mottu.Rentals.Api.Data;
using Mottu.Rentals.Api.Entities;

namespace Mottu.Rentals.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntregadorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EntregadorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entregador>>> GetAll()
        {
            return await _context.Entregadores.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Entregador>> Create(Entregador entregador)
        {
            _context.Entregadores.Add(entregador);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = entregador.Id }, entregador);
        }
    }
}
