namespace Mottu.Rentals.Api.DTO
{
    public class LocacaoResponseDto
    {
        public int Id { get; set; }
        public string MotoPlaca { get; set; }
        public string EntregadorNome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFimPrevista { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public decimal ValorPrevisto { get; set; }
        public decimal? ValorFinal { get; set; }
    }
}
