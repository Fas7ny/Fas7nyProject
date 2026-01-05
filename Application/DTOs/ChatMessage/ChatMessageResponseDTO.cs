using Fas7ny.Application.DTOs.Account.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fas7ny.Application.Dtos.ChatBoxDtos
{
    public class ChatMessageResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MessageText { get; set; } = string.Empty;
        public string? ResponseText { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public virtual UserResponseDto User { get; set; } = null!;
    }
}
