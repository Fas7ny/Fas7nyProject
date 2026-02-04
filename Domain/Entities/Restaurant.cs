namespace Fas7ny.Domain.Entities
{
    public class Restaurant
    {
        public string address;
        public string imageUrl;

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Cuisine { get; set; }
        public string? Description { get; set; }
        public string? PriceRange { get; set; }
        public int CityId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public virtual City City { get; set; } = null!;
    }
}
