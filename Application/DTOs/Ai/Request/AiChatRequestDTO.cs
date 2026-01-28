using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Ai.Request
{
    public class AiChatRequestDto
    {
        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [StringLength(2000, MinimumLength = 1, ErrorMessage = "Message must be between 1 and 2000 characters")]
        public string Message { get; set; }


        [StringLength(50, ErrorMessage = "Context type cannot exceed 50 characters")]
        public string? ContextType { get; set; }

        public int? ContextItemId { get; set; }

        public bool IncludeRecommendations { get; set; } = true;
    }
}
