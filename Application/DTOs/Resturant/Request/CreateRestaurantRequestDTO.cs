using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fas7ny.Application.DTOs.Resturant.Request
{
    public class CreateRestaurantRequestDTO
    {
        [Required(ErrorMessage = "Restaurant name is required")]
        [StringLength(200, ErrorMessage = "Restaurant name cannot exceed 200 characters")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "Cuisine cannot exceed 100 characters")]
        public string Cuisine { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }

        [RegularExpression(@"^\$+$", ErrorMessage = "Price range must be $, $$, $$$, or $$$$")]
        [StringLength(4, ErrorMessage = "Price range cannot exceed 4 characters")]
        public string PriceRange { get; set; }

        [Required(ErrorMessage = "City ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid city ID is required")]
        public int CityId { get; set; }
    }

}
