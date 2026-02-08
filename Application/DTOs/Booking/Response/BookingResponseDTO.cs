namespace Fas7ny.Application.DTOs.Booking.Response
{
    public class BookingResponseDTO
    {
        public int Id { get; set; }
        public string BookingType { get; set; } = null!;
        public int BookingItemId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
