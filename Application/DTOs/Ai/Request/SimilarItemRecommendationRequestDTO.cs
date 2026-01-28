using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Ai.Request
{
    public class SimilarItemRecommendationRequestDTO
    {
        [Required(ErrorMessage = "Item type is required")]
        [RegularExpression("^(Package|Hotel|Restaurant|TouristPlace)$",
            ErrorMessage = "Invalid item type")]
        public string ItemType { get; set; }

        [Required(ErrorMessage = "Item ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Item ID must be a positive number")]
        public int ItemId { get; set; }

        [Range(1, 20, ErrorMessage = "Number of recommendations must be between 1 and 20")]
        public int NumberOfRecommendations { get; set; } = 5;

        [StringLength(450, ErrorMessage = "User ID cannot exceed 450 characters")]
        public string? UserId { get; set; }
    }
}
