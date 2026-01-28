using Microsoft.AspNetCore.Identity;

namespace Fas7ny.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string? PreferencesJson { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
        public ICollection<SearchLog> SearchLogs { get; set; } = new List<SearchLog>();
        public ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<UserInteraction> UserInteractions { get; set; } = new List<UserInteraction>();
        public ICollection<UserPreference> UserPreferences { get; set; } = new List<UserPreference>();



        public string Role { get; set; }
        public Carts? Cart { get; set; }
    }
}
