using Fas7ny.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fas7ny.Application.DTOs.Hotel.Response
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
        public string ImageUrl { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }

    }
}
