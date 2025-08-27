using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mottu.Rentals.Api.Data;
using Mottu.Rentals.Api.DTO;
using Mottu.Rentals.Api.Entities;
using Mottu.Rentals.Api.Services;

namespace Mottu.Rentals.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocacaoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private RentalService _rentalService;

        public LocacaoController(AppDbContext context)
        {
            _context = context;
            _rentalService = new RentalService(_context);
        }



        [HttpPost]
        public async Task<ActionResult<RentalResponseDto>> CreateRental([FromBody] RentalCreateDto dto)
        {
            var result = await _rentalService.CreateRentalAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RentalResponseDto>> GetById(Guid id)
        {
            var rental = await _rentalService.GetRentalByIdAsync(id);
            if (rental == null) return NotFound();
            return Ok(rental);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalResponseDto>>> List()
        {
            var rentals = await _rentalService.ListRentalsAsync();
            return Ok(rentals);
        }

        [HttpPut("{id}/return")]
        public async Task<ActionResult<RentalResponseDto>> ReturnBike(Guid id, [FromBody] RentalReturnDto dto)
        {
            var result = await _rentalService.FinalizeRentalAsync(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var success = await _rentalService.CancelRentalAsync(id);
            if (!success) return BadRequest("Unable to cancel rental.");
            return NoContent();
        }
    }
}

