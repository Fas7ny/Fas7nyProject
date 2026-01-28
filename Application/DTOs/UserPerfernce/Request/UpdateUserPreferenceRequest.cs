using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.UserPerfernce.Request
{
    public class UpdateUserPreferenceRequest
    {
        [Required(ErrorMessage = "Preference ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Preference ID must be a positive number")]
        public int Id { get; set; }

        [Range(1, 365, ErrorMessage = "Stay duration must be between 1 and 365 days")]
        public int? StayDuration { get; set; }

        [Range(1, 10000000, ErrorMessage = "Budget must be between 1 and 10,000,000")]
        public decimal? Budget { get; set; }

        [StringLength(50, ErrorMessage = "Category preference cannot exceed 50 characters")]
        [RegularExpression("^(Adventure|Relaxation|Cultural|Beach|Mountain|Urban|Rural|Luxury|Budget|Family|Romantic|Solo)$",
            ErrorMessage = "Invalid category")]
        public string? CategoryPreference { get; set; }
    }
}
