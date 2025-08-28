using System.ComponentModel.DataAnnotations;

namespace Mottu.Rentals.Api.DTO
{
    public class RiderUpdateDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter 11 dígitos numéricos.")]
        public string Cpf { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "O CNPJ deve conter 14 dígitos numéricos.")]
        public string Cnpj { get; set; } = string.Empty;

        [Required(ErrorMessage = "O número da CNH é obrigatório.")]
        [StringLength(20, ErrorMessage = "O número da CNH deve ter no máximo 20 caracteres.")]
        public string LicenseNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "A categoria da CNH é obrigatória.")]
        [RegularExpression(@"^(A|B|AB|AC|AD|AE)$", ErrorMessage = "Categoria inválida de CNH.")]
        public string LicenseType { get; set; } = string.Empty;

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }


        public string? LicenseImage { get; set; }

        public string? Email { get; set; }
    }

}
