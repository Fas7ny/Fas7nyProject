using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Booking.Request
{
    public class CreateBookingRequestDTO
    {
        [Required]
        [StringLength(50)]
        public string BookingType { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue)]
        public int BookingItemId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
