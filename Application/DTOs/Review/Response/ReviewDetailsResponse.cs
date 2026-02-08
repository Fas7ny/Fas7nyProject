namespace Fas7ny.Application.DTOs.Review.Response
{
    public class ReviewDetailsResponse
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsVerifiedPurchase { get; set; }
    }
}
