namespace Mottu.Rentals.Api.Entities
{
    public class Moto
    {
        public Guid Id { get; set; }
        public string Modelo { get; set; } = null!;
        public int Ano { get; set; }
        public string Placa { get; set; } = null!;
    }
}
