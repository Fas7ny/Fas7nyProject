namespace Fas7ny.Application.DTOs.Payment.Request
{
    public class PaymobWebhookData
    {
        public int OrderId { get; set; }
        public string? MerchantOrderId { get; set; }
        public bool Success { get; set; }
    }
}
