using Fas7ny.Domain.Entities.Fas7ny.Domain.Entities;

namespace Fas7ny.Domain.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();
        public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
        public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
        public virtual ICollection<TouristPlace> TouristPlaces { get; set; } = new List<TouristPlace>();
        public virtual ICollection<Package> Packages { get; set; } = new List<Package>();
    }
}
