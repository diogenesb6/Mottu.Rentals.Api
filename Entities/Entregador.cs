namespace Mottu.Rentals.Api.Entities
{
    public class Entregador
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string CNPJ { get; set; } = null!; // único
        public DateTime DataNascimento { get; set; }
        public string NumeroCNH { get; set; } = null!; // único
        public string TipoCNH { get; set; } = null!; // A, B ou A+B
        public string? ImagemCNH { get; set; } // URL ou path do arquivo
    }
}
