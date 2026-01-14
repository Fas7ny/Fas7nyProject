using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Hotel.Request
{
    public class UpdateHotelDto
    {
        [StringLength(200, ErrorMessage = "Hotel name cannot exceed 200 characters")]
        public string Name { get; set; }

        [StringLength(300, ErrorMessage = "Address cannot exceed 300 characters")]
        public string Address { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }

        [Range(0.01, 100000, ErrorMessage = "Price per night must be between 0.01 and 100,000")]
        public decimal? PricePerNight { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        public string ImageUrl { get; set; }
    }

}
