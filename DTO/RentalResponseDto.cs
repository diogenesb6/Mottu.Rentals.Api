namespace Mottu.Rentals.Api.DTO
{
    public class RentalResponseDto
    {
        public Guid Id { get; set; }
        public Guid BikeId { get; set; }
        public string BikeModel { get; set; } = null!;
        public string BikePlate { get; set; } = null!;
        public Guid RiderId { get; set; }
        public string RiderName { get; set; } = null!;
        public string RiderLicenseNumber { get; set; } = null!;
        public int PlanDays { get; set; }
        public decimal DailyRate { get; set; }
        public decimal ExpectedValue { get; set; }
        public decimal? FinalValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
