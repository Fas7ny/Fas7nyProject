using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Booking.Request
{
    public class UpdateBookingStatusDTO
    {
        [Required]
        [RegularExpression("^(Pending|Confirmed|Cancelled|Completed)$")]
        public string Status { get; set; } = null!;
    }
}
