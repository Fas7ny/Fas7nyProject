using Fas7ny.Application.DTOs.Account.Response;

namespace Fas7ny.Application.DTOs.Booking.Response
{
    public class BookingResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string BookingType { get; set; } = string.Empty;
        public int BookingItemId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";

        public virtual UserResponseDto User { get; set; } = null!;
    }

}
