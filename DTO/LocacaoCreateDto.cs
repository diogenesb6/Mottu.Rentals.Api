namespace Mottu.Rentals.Api.DTO
{
    public class LocacaoCreateDto
    {
        public int MotoId { get; set; }
        public int EntregadorId { get; set; }
        public int PlanoDias { get; set; } // 7, 15, 30, 45, 50
        public DateTime DataInicio { get; set; } // primeiro dia após criação
    }
}
