using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Payment.Request
{
    public class CreatePaymentRequest
    {
        [Required(ErrorMessage = "Booking ID is required")]
        public Guid BookingId { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Amount must be between 0.01 and 999,999.99")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Payment method is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Payment method must be between 3 and 50 characters")]
        [RegularExpression("^(CreditCard|DebitCard|PayPal|Cash|BankTransfer|Stripe)$",
            ErrorMessage = "Invalid payment method. Allowed: CreditCard, DebitCard, PayPal, Cash, BankTransfer, Stripe")]
        public string PaymentMethod { get; set; }

        [Required(ErrorMessage = "Payment date is required")]
        [DataType(DataType.DateTime)]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    }
}
