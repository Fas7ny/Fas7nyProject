using Fas7ny.Application.DTOs.Account.Response;

namespace Fas7ny.Application.DTOs.ChatMessage.Response
{
    public class ChatMessageResponseDTO
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
    }
}
