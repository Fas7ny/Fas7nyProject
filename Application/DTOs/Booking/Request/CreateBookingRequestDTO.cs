using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Booking.Request
{
    public class CreateBookingRequestDTO
    {
        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }

        public string BookingType { get; set; }

        [Required(ErrorMessage = "Booking Item ID is required")]
        public string BookingItemId { get; set; } // خليها int

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Total amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than 0")]
        public decimal TotalAmount { get; set; }
    }

}