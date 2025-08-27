namespace Mottu.Rentals.Api.Entities
{
    public class Bike
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Year { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Plate { get; set; } = string.Empty;

        public bool HasActiveRental { get; set; } = false;
    }
}
