using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Ai.Request
{
    public class GeneratePackageRequestDTO
    {
        [Required(ErrorMessage = "User ID is required")]
        [StringLength(450, ErrorMessage = "User ID cannot exceed 450 characters")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Destination city is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "City name must be between 2 and 100 characters")]
        public string DestinationCity { get; set; }

        [StringLength(100, ErrorMessage = "Country name cannot exceed 100 characters")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(1, 30, ErrorMessage = "Duration must be between 1 and 30 days")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "Budget is required")]
        [Range(100, 1000000, ErrorMessage = "Budget must be between 100 and 1,000,000")]
        public decimal Budget { get; set; }

        [Required(ErrorMessage = "Number of travelers is required")]
        [Range(1, 20, ErrorMessage = "Number of travelers must be between 1 and 20")]
        public int NumberOfTravelers { get; set; }

        [Required(ErrorMessage = "Travel style is required")]
        [RegularExpression("^(Adventure|Relaxation|Cultural|Beach|Mountain|Urban|Rural|Luxury|Budget|Family|Romantic|Solo|Business)$",
            ErrorMessage = "Invalid travel style")]
        public string TravelStyle { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PreferredStartDate { get; set; }

        [StringLength(500, ErrorMessage = "Special requirements cannot exceed 500 characters")]
        public string? SpecialRequirements { get; set; }

        // Preferences
        public List<string> PreferredActivities { get; set; } = new List<string>();

        [Range(1, 5, ErrorMessage = "Accommodation rating must be between 1 and 5")]
        public int? PreferredAccommodationRating { get; set; }

        public bool IncludeFlights { get; set; } = false;

        public bool IncludeMeals { get; set; } = false;

        [RegularExpression("^(None|Breakfast|HalfBoard|FullBoard|AllInclusive)?$",
            ErrorMessage = "Invalid meal plan")]
        public string? MealPlan { get; set; }

        public bool IncludeTransportation { get; set; } = false;

        public bool IncludeGuide { get; set; } = false;
    }
}
