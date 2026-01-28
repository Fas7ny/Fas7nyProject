namespace Fas7ny.Domain.Entities
{
    namespace Fas7ny.Domain.Entities
    {
        public class Country
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Code { get; set; } = string.Empty;
            public bool IsActive { get; set; }

            // Navigation
            public ICollection<City> Cities { get; set; } = new List<City>();
        }
    }

}
