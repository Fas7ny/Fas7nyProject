using Fas7ny.Application.DTOs.Hotel.Response;
using Fas7ny.Application.DTOs.Resturant.Response;
using Fas7ny.Application.DTOs.TouristPlace.Response;

namespace Fas7ny.Application.DTOs.City.Response
{
    public class CityDetailResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int TouristPlaceCount { get; set; }
    }

}
