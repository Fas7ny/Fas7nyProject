namespace Fas7ny.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int PackageId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Package? Package { get; set; }
        public ApplicationUser User { get; set; }  // ✅ FIXED - Capital 'U'
    }
}