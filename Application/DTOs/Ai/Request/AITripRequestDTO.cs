using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Ai.Request
{
    public class AITripRequestDTO
    {
        [Required(ErrorMessage = "Prompt is required")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Prompt must be between 10 and 1000 characters")]
        public string Prompt { get; set; }

        public string UserId { get; set; }
    }
}
