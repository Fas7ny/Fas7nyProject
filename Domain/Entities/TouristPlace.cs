namespace Fas7ny.Domain.Entities
{
    public class TouristPlace
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? OpeningHours { get; set; }
        public decimal EntryFee { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual ICollection<PackageDetail> PackageDetails { get; set; } = new List<PackageDetail>();
    }
}
