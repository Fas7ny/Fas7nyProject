using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.ChatMessage.Request
{
    public class CreateChatMessageDTO
    {
        [Required(ErrorMessage = "Sender ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid Sender ID is required")]
        public int SenderId { get; set; }

        [Required(ErrorMessage = "Receiver ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid Receiver ID is required")]
        public int ReceiverId { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [StringLength(1000, ErrorMessage = "Message cannot exceed 1000 characters")]
        [MinLength(1, ErrorMessage = "Message cannot be empty")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Timestamp is required")]
        public DateTime Timestamp { get; set; }
    }
}
