namespace Fas7ny.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
        public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
        public ICollection<TouristPlace> TouristPlaces { get; set; } = new List<TouristPlace>();
        public ICollection<Package> Packages { get; set; } = new List<Package>();
    }
}
