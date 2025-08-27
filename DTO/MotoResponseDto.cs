namespace Mottu.Rentals.Api.DTO
{
    public class MotoResponseDto
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Plate { get; set; } = string.Empty;
    }
}
