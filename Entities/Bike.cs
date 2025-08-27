using System.ComponentModel.DataAnnotations;

namespace Mottu.Rentals.Api.Entities
{
    public class Bike
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string Plate { get; set; } = null!; 
    }
}
