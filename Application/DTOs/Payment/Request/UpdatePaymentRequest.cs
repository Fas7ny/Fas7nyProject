using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Payment.Request
{
    public class UpdatePaymentRequest
    {
        [Required(ErrorMessage = "Payment ID is required")]
        public Guid Id { get; set; }

        [Range(0.01, 999999.99, ErrorMessage = "Amount must be between 0.01 and 999,999.99")]
        public decimal? Amount { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Payment method must be between 3 and 50 characters")]
        public string? PaymentMethod { get; set; }
    }
}
