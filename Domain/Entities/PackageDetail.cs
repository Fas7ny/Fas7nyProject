namespace Fas7ny.Domain.Entities
{
    public class PackageDetail
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int TouristPlaceId { get; set; }
        public int DayOrder { get; set; }
        public string? Description { get; set; }

        public virtual Package Package { get; set; } = null!;
        public virtual TouristPlace TouristPlace { get; set; } = null!;
    }
}
