namespace Fas7ny.Domain.Entities
{
    public class Activity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Cost { get; private set; }
        public Guid CityId { get; private set; }

        private Activity() { }

        public Activity(string name, decimal cost, Guid cityId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Cost = cost;
            CityId = cityId;
        }
    }

}
