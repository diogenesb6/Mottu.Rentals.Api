using System.ComponentModel.DataAnnotations;

namespace Mottu.Rentals.Api.DTO
{
    public class RiderCreateDto
    {

        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(14)]
        public string Cpf { get; set; }

        [Required]
        [StringLength(18)]
        public string LicenseNumber { get; set; }

        [Required]
        public string LicenseType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CnhValidade { get; set; }

       
        public string? LicenseImage { get; set; }
    }
}