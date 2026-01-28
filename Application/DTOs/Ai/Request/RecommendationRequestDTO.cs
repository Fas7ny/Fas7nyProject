using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Ai.Request
{
    public class RecommendationRequestDTO
    {
        [Required(ErrorMessage = "User ID is required")]
        [StringLength(450, ErrorMessage = "User ID cannot exceed 450 characters")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Recommendation type is required")]
        [RegularExpression("^(Destination|Package|Hotel|Restaurant|Activity|Similar)$",
            ErrorMessage = "Invalid recommendation type. Allowed: Destination, Package, Hotel, Restaurant, Activity, Similar")]
        public string RecommendationType { get; set; }

        [Range(1, 50, ErrorMessage = "Number of recommendations must be between 1 and 50")]
        public int NumberOfRecommendations { get; set; } = 10;

        [Range(100, 1000000, ErrorMessage = "Budget must be between 100 and 1,000,000")]
        public decimal? Budget { get; set; }

        [Range(1, 30, ErrorMessage = "Duration must be between 1 and 30 days")]
        public int? DurationDays { get; set; }

        [RegularExpression("^(Adventure|Relaxation|Cultural|Beach|Mountain|Urban|Rural|Luxury|Budget|Family|Romantic|Solo)?$",
            ErrorMessage = "Invalid travel style")]
        public string? TravelStyle { get; set; }

        [StringLength(100, ErrorMessage = "Destination cannot exceed 100 characters")]
        public string? Destination { get; set; }

        public List<string> Preferences { get; set; } = new List<string>();

        public List<int> ExcludeItemIds { get; set; } = new List<int>();

        public bool UsePersonalizedData { get; set; } = true;

        public bool IncludeSimilarToVisited { get; set; } = false;
    }
}
