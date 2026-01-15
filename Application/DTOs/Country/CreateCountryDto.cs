using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.Dtos.CountryDtos
{
    public class CreateCountryDto
    {
        [Required(ErrorMessage = "Country name is required")]
        [StringLength(100, ErrorMessage = "Country name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Country code is required")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "Country code must be 2-3 characters")]
        [RegularExpression("^[A-Z]{2,3}$", ErrorMessage = "Country code must be 2-3 uppercase letters")]
        public string Code { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }
    }
}
