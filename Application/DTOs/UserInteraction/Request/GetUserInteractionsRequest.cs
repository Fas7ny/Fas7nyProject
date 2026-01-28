using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.UserInteraction.Request
{
    public class GetUserInteractionsRequest
    {
        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }

        [StringLength(50, ErrorMessage = "Item type cannot exceed 50 characters")]
        [RegularExpression("^(TouristPlace|Hotel|Package|Restaurant|City|HotelRoom)?$",
            ErrorMessage = "Invalid item type")]
        public string? ItemType { get; set; }

        [StringLength(50, ErrorMessage = "Interaction type cannot exceed 50 characters")]
        public string? InteractionType { get; set; }

        [Range(1, 1000, ErrorMessage = "Limit must be between 1 and 1000")]
        public int Limit { get; set; } = 50;

        [Range(0, int.MaxValue, ErrorMessage = "Offset must be non-negative")]
        public int Offset { get; set; } = 0;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
