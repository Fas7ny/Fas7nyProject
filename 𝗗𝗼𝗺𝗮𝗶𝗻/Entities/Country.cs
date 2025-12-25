using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Country
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Currency { get; private set; }

        private Country() { }

        public Country(string name, string currency)
        {
            Id = Guid.NewGuid();
            Name = name;
            Currency = currency;
        }
    }

}
