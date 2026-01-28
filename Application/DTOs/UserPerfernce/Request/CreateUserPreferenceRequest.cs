using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.UserPerfernce.Request
{
    public class CreateUserPreferenceRequest
    {
        [Required(ErrorMessage = "User ID is required")]
        [StringLength(450, ErrorMessage = "User ID cannot exceed 450 characters")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Stay duration is required")]
        [Range(1, 365, ErrorMessage = "Stay duration must be between 1 and 365 days")]
        public int StayDuration { get; set; }

        [Required(ErrorMessage = "Budget is required")]
        [Range(1, 10000000, ErrorMessage = "Budget must be between 1 and 10,000,000")]
        public decimal Budget { get; set; }

        [Required(ErrorMessage = "Category preference is required")]
        [StringLength(50, ErrorMessage = "Category preference cannot exceed 50 characters")]
        [RegularExpression("^(Adventure|Relaxation|Cultural|Beach|Mountain|Urban|Rural|Luxury|Budget|Family|Romantic|Solo)$",
            ErrorMessage = "Invalid category. Allowed: Adventure, Relaxation, Cultural, Beach, Mountain, Urban, Rural, Luxury, Budget, Family, Romantic, Solo")]
        public string CategoryPreference { get; set; }
    }
}
