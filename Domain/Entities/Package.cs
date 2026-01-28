namespace Fas7ny.Domain.Entities
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
        public string? ImageUrl { get; set; }
        public int CityId { get; set; }
        public int HotelId { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual Hotel Hotel { get; set; } = null!;
        public virtual ICollection<PackageDetail> PackageDetails { get; set; } = new List<PackageDetail>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

}
