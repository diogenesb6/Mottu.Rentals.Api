using System.ComponentModel.DataAnnotations;

namespace Mottu.Rentals.Api.Entities
{
    public class Rider
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(14)]
        public string Cpf { get; set; } = null!; 

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string LicenseNumber { get; set; } = null!; 

        [Required]
        [MaxLength(2)]
        public string LicenseType { get; set; } = null!;

        public string? LicenseImage { get; set; } 
    }
}
