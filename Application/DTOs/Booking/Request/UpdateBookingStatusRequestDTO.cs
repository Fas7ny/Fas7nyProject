using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Booking.Request
{
    public class UpdateBookingStatusDTO
    {
        [Required(ErrorMessage = "Booking ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid Booking ID is required")]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        [RegularExpression("^(Pending|Confirmed|Cancelled|Completed)$",
            ErrorMessage = "Status must be Pending, Confirmed, Cancelled, or Completed")]
        public string Status { get; set; }


    }
}
