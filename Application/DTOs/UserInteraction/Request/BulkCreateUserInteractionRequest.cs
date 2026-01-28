using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.UserInteraction.Request
{
    public class BulkCreateUserInteractionRequest
    {
        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "At least one interaction is required")]
        [MinLength(1, ErrorMessage = "At least one interaction is required")]
        [MaxLength(100, ErrorMessage = "Cannot create more than 100 interactions at once")]
        public List<UserInteractionItem> Interactions { get; set; } = new List<UserInteractionItem>();
    }

    public class UserInteractionItem
    {
        [Required]
        public string ItemType { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ItemId { get; set; }

        [Required]
        public string InteractionType { get; set; }
    }
}
