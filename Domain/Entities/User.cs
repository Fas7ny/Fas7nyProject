using Microsoft.AspNetCore.Identity;

namespace Fas7ny.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public string? PreferencesJson { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
        public virtual ICollection<SearchLog> SearchLogs { get; set; } = new List<SearchLog>();
        public virtual ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
    }

}
