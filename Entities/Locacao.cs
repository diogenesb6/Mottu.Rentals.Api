using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mottu.Rentals.Api.Entities
{
    public class Locacao
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid MotoId { get; set; }
        public Moto Moto { get; set; }

        [Required]
        public Guid EntregadorId { get; set; }
        public Entregador Entregador { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataPrevistaTermino { get; set; }

        // Data real de devolução (null enquanto não devolvida)
        public DateTime? DataTermino { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorDiaria { get; set; }

        [Required]
        public int PlanoDias { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorPrevisto { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? ValorFinal { get; set; }

        public bool Ativa { get; set; } = true;
    }
}
