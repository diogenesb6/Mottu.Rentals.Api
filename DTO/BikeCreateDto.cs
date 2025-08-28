using System.ComponentModel.DataAnnotations;

namespace Mottu.Rentals.Api.DTO
{
    public class BikeCreateDto
    {
        [Required]
        public int Year { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string Plate { get; set; } = string.Empty;
    }
}
