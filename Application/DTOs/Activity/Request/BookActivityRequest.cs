namespace Fas7ny.Application.DTOs.Activity.Request
{
    public class BookActivityRequest
    {
        public DateTime BookingDate { get; set; }
        public int NumberOfPeople { get; set; } = 1;
        public int? DurationHours { get; set; } = 2; // Default 2 hours
        public string? SpecialRequests { get; set; }
    }
}
