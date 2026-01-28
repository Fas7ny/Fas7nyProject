namespace Fas7ny.Domain.Entities
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string MessageText { get; set; } = string.Empty;
        public string? ResponseText { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public virtual ApplicationUser User { get; set; } = null!;
    }
}
