using Fas7ny.Enum;
using System.Text.Json.Serialization;

namespace Fas7ny.Domain.Entities
{
    public class UserPreference
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public int StayDuration { get; set; }
        public decimal Budget { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Category CategoryPreference { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ApplicationUser User { get; set; }
    }
}
