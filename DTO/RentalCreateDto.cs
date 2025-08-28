namespace Mottu.Rentals.Api.DTO
{
    public class RentalCreateDto
    {
        public Guid BikeId { get; set; }
        public Guid RiderId { get; set; }
        public int PlanDays { get; set; }
    }
}
