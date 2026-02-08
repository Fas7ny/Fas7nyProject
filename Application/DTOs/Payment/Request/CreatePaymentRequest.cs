namespace Fas7ny.Application.DTOs.Payment.Request
{
    public class CreatePaymentRequest
    {
        public int BookingId { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }

}
