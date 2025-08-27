using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mottu.Rentals.Api.Data;
using Mottu.Rentals.Api.DTO;
using Mottu.Rentals.Api.Entities;

namespace Mottu.Rentals.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocacaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LocacaoController(AppDbContext context)
        {
            _context = context;
        }



            [HttpPost]
            public async Task<ActionResult<LocacaoResponseDto>> CriarLocacao([FromBody] LocacaoCreateDto dto)
            {
                var result = await _locacaoService.CriarLocacaoAsync(dto);
                return CreatedAtAction(nameof(ObterPorId), new { id = result.Id }, result);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<LocacaoResponseDto>> ObterPorId(int id)
            {
                var locacao = await _locacaoService.ObterPorIdAsync(id);
                if (locacao == null) return NotFound();
                return Ok(locacao);
            }

  
            [HttpGet]
            public async Task<ActionResult<IEnumerable<LocacaoResponseDto>>> Listar()
            {
                var lista = await _locacaoService.ListarAsync();
                return Ok(lista);
            }

     
            [HttpPut("{id}/devolucao")]
            public async Task<ActionResult<LocacaoResponseDto>> DevolverMoto(int id, [FromBody] LocacaoDevolucaoDto dto)
            {
                var result = await _locacaoService.DevolverMotoAsync(id, dto);
                if (result == null) return NotFound();
                return Ok(result);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Cancelar(int id)
            {
                var sucesso = await _locacaoService.CancelarLocacaoAsync(id);
                if (!sucesso) return BadRequest("Não foi possível cancelar a locação.");
                return NoContent();
            }
        }
}
