using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Place
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public Guid CityId { get; private set; }

        private Place() { }

        public Place(string name, string description, decimal price, Guid cityId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            CityId = cityId;
        }
    }

}
