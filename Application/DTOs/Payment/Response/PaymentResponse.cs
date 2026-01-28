namespace Fas7ny.Application.DTOs.Payment.Response
{
    public class PaymentResponse
    {
        public string PaymentKey { get; set; } = string.Empty;
        public int OrderId { get; set; }
        public string PaymentUrl { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }

}
