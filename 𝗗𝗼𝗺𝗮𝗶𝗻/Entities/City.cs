using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class City
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid CountryId { get; private set; }

        private City() { }

        public City(string name, Guid countryId)
        {
            Id = Guid.NewGuid();
            Name = name;
            CountryId = countryId;
        }
    }

}
