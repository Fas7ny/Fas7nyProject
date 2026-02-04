using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Hotel.Request
{
    public class UpdateHotelDto
    {
        [Required(ErrorMessage = "Hotel ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid Hotel ID is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Hotel name is required")]
        [StringLength(100, ErrorMessage = "Hotel name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid City ID is required")]
        public int CityId { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
        public decimal Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }
        public decimal price { get; set; }
    }


}
