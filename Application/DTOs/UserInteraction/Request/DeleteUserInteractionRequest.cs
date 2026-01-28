using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.UserInteraction.Request
{
    public class DeleteUserInteractionRequest
    {
        [Required(ErrorMessage = "Interaction ID is required")]
        public int InteractionId { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }
    }

}
