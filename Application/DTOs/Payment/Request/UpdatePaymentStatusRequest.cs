using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Payment.Request
{
    public class ProcessRefundRequest
    {
        [Required(ErrorMessage = "Payment ID is required")]
        public Guid PaymentId { get; set; }

        [Required(ErrorMessage = "Refund amount is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Refund amount must be between 0.01 and 999,999.99")]
        public decimal RefundAmount { get; set; }

        [StringLength(500, ErrorMessage = "Refund reason cannot exceed 500 characters")]
        public string? RefundReason { get; set; }
    }
}
