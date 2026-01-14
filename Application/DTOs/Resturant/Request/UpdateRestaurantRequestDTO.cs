using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Resturant.Request
{
    public class UpdateRestaurantRequestDTO
    {
        [StringLength(200, ErrorMessage = "Restaurant name cannot exceed 200 characters")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "Cuisine cannot exceed 100 characters")]
        public string Cuisine { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }

        [RegularExpression(@"^\$+$", ErrorMessage = "Price range must be $, $$, $$$, or $$$$")]
        [StringLength(4, ErrorMessage = "Price range cannot exceed 4 characters")]
        public string PriceRange { get; set; }
    }
}
