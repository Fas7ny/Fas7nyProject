using Fas7ny.Application.DTOs.Common.Response;

namespace Fas7ny.Application.DTOs.Payment.Response
{
    public class PaymentDetailsResponse : ApiResponse
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public override bool Success { get; set; } = true;
        public override string Message { get; set; } = "Payment details retrieved successfully.";


        // Booking Details
        public string BookingType { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        public decimal BookingTotalPrice { get; set; }

        // User Details
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserFullName { get; set; }
    }

}
