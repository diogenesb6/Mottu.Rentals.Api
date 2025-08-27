using System.ComponentModel.DataAnnotations;

namespace Mottu.Rentals.Api.DTO
{
    public class EntregadorCreateDto
    {

        [Required]
        public string Nome { get; set; }
        [Required]
        [StringLength(14)]
        public string Cpf { get; set; }

        [Required]
        [StringLength(18)]
        public string CnhNumero { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CnhValidade { get; set; }
        public IFormFile? FotoCnh { get; set; }
    }

    public class EntregadorReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string CnhNumero { get; set; }
        public string FotoCnhUrl { get; set; }
    }

    public class EntregadorUpdateDto
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        [StringLength(18)]
        public string CnhNumero { get; set; }

        [DataType(DataType.Date)]
        public DateTime CnhValidade { get; set; }

        public IFormFile? FotoCnh { get; set; }
    }
}