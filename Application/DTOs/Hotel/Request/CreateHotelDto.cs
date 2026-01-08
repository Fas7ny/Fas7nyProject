using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fas7ny.Application.DTOs.Hotel.Request
{
    public class CreateHotelDto
    {
        [Required(ErrorMessage = "Hotel name is required")]
        [StringLength(200, ErrorMessage = "Hotel name cannot exceed 200 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(300, ErrorMessage = "Address cannot exceed 300 characters")]
        public string Address { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price per night is required")]
        [Range(0.01, 100000, ErrorMessage = "Price per night must be between 0.01 and 100,000")]
        public decimal PricePerNight { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "City ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid city ID is required")]
        public int CityId { get; set; }
    }

}
