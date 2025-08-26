namespace Mottu.Rentals.Api.Entities
{
    public class Locacao
    {
        public Guid Id { get; set; }
        public Guid MotoId { get; set; }
        public Guid EntregadorId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public DateTime DataPrevistaTermino { get; set; }
        public decimal ValorDiaria { get; set; }
        public int PlanoDias { get; set; }
        public bool Ativa { get; set; } = true;
    }
}
