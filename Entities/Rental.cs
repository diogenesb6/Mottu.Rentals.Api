using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mottu.Rentals.Api.Entities
{
    public class Rental
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid BikeId { get; set; }
        public Bike Bike { get; set; } = null!;

        [Required]
        public Guid RiderId { get; set; }
        public Rider Rider { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime ExpectedEndDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal DailyRate { get; set; }

        [Required]
        public int PlanDays { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal ExpectedValue { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? FinalValue { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
