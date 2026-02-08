namespace Fas7ny.Domain.Entities
{
    public class Activity
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public decimal Cost { get; private set; }
        public int CityId { get; private set; }
        public string ImageUrl { get; set; }
        // public int TouristPlacesId { get; set; }

        // Navigation property
        public virtual City City { get; set; } = null!;

        // Parameterless constructor for EF Core
        private Activity() { }

        // Constructor
        public Activity(string name, decimal cost, int cityId)
        {
            Name = name;
            Cost = cost;
            CityId = cityId;
        }

        // Update method
        //public void Update(string name, decimal cost, int cityId)
        //{
        //    Name = name;
        //    Cost = cost;
        //    CityId = cityId;
        //}
    }


}
