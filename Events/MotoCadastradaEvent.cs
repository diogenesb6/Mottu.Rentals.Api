namespace Mottu.Rentals.Api.Events
{
    public class MotoCadastradaEvent
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Plate { get; set; } = string.Empty;
    }
}
