using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mottu.Rentals.Api.Data;
using Mottu.Rentals.Api.DTO;
using Mottu.Rentals.Api.Entities;
using Mottu.Rentals.Api.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
//using Mottu.Rentals.Api.Events;

namespace Mottu.Rentals.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private static List<Bike> _motos = new();


        public MotoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMoto([FromBody] MotoRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verifica se já existe moto com a mesma placa
            var exists = await _context.Motos.AnyAsync(m => m.Plate == dto.Plate);
            if (exists)
                return Conflict(new { message = "Já existe uma moto com essa placa." });

            var moto = new Bike
            {
                Year = dto.Year,
                Model = dto.Model,
                Plate = dto.Plate
            };

            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();


            // Publica evento no RabbitMQ
            /*
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    Port = 5672, // Porta explícita
                    UserName = "guest", // Credenciais padrão
                    Password = "guest",
                   // DispatchConsumersAsync = true
                };

                using var connection = factory.CreateConnectionAsync();
                using var channel =  connection.;

                channel.QueueDeclare(
                    queue: "MotoCadastrada",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var evento = new MotoCadastradaEvent
                {
                    Id = moto.Id,
                    Year = moto.Year,
                    Model = moto.Model,
                    Plate = moto.Plate
                };

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(evento));

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "MotoCadastrada",
                    basicProperties: null,
                    body: body
                );
            }
            catch (Exception ex)
            {

                return BadRequest(new { message ="Erro ao publicar evento no RabbitMQ" });
            }*/


            var response = new MotoResponseDto
            {
                Id = moto.Id,
                Year = moto.Year,
                Model = moto.Model,
                Plate = moto.Plate
            };

            return CreatedAtAction(nameof(GetMotoById), new { id = moto.Id }, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetMotos([FromQuery] string? plate)
        {
            var query = _context.Motos.AsQueryable();

            if (!string.IsNullOrEmpty(plate))
                query = query.Where(m => m.Plate.Contains(plate));

            var motos = await query
                .Select(m => new MotoResponseDto
                {
                    Id = m.Id,
                    Year = m.Year,
                    Model = m.Model,
                    Plate = m.Plate
                })
                .ToListAsync();

            return Ok(motos);
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetMotoById(Guid id)
        {
            var moto = await _context.Motos.FindAsync(id);

            if (moto == null)
                return NotFound(new { message = "Moto não encontrada." });

            var response = new MotoResponseDto
            {
                Id = moto.Id,
                Year = moto.Year,
                Model = moto.Model,
                Plate = moto.Plate
            };

            return Ok(response);
        }

        // PUT: api/motos/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateMotoPlate(Guid id, [FromBody] string newPlate)
        {
            if (string.IsNullOrWhiteSpace(newPlate))
                return BadRequest(new { message = "A placa não pode ser vazia." });

            var moto = await _context.Motos.FindAsync(id);
            if (moto == null)
                return NotFound(new { message = "Moto não encontrada." });

            // Verifica se já existe outra moto com a mesma placa
            var exists = await _context.Motos.AnyAsync(m => m.Plate == newPlate && m.Id != id);
            if (exists)
                return Conflict(new { message = "Já existe outra moto com essa placa." });

            moto.Plate = newPlate;
            await _context.SaveChangesAsync();

            var response = new MotoResponseDto
            {
                Id = moto.Id,
                Year = moto.Year,
                Model = moto.Model,
                Plate = moto.Plate
            };

            return Ok(response);
        }


        // DELETE: api/motos/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteMoto(Guid id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null)
                return NotFound(new { message = "Moto não encontrada." });

            // Verifica se existe locação ativa
            if (moto.HasActiveRental)
                return BadRequest(new { message = "Não é possível excluir uma moto com locações ativas." });

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
