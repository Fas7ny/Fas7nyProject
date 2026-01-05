using Fas7ny.Application.DTOs.City.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fas7ny.Application.DTOs.Resturant.Response
{
    public class RestaurantResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Cuisine { get; set; }
        public string? Description { get; set; }
        public string? PriceRange { get; set; }
        public int CityId { get; set; }

        public virtual CreateCityRequestDTO City { get; set; } = null!;
    }
}
