namespace Mottu.Rentals.Api.Entities
{
    public class Rider
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Cpf { get; set; } = null!; // único
        public DateTime BirthDate { get; set; }
        public string LicenseNumber { get; set; } = null!; // único
        public string LicenseType { get; set; } = null!; // A, B ou A+B
        public string? LicenseImage { get; set; } // URL ou path do arquivo
    }
}
