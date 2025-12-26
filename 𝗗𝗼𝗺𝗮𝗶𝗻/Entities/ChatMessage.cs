using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fas7ny.Domain.Entities
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MessageText { get; set; } = string.Empty;
        public string? ResponseText { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public virtual User User { get; set; } = null!;
    }
}
