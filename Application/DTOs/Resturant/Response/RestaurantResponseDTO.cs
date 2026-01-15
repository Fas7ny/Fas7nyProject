using Fas7ny.Application.DTOs.City.Request;

namespace Fas7ny.Application.DTOs.Resturant.Response
{
    public class RestaurantResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CuisineType { get; set; }
        public decimal Rating { get; set; }
        public string PhoneNumber { get; set; }
    }
}
