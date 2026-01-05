using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fas7ny.Application.DTOs.Activity.Response
{
    public class ActivityResponseDTO
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Cost { get; private set; }
        public Guid CityId { get; private set; }

        private ActivityResponseDTO() { }

        public ActivityResponseDTO(string name, decimal cost, Guid cityId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Cost = cost;
            CityId = cityId;
        }
    }

}
