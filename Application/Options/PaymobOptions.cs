namespace Fas7ny.Application.Services.Payment
{
    public class PaymobOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public int IntegrationId { get; set; }
        public string Currency { get; set; } = "EGP";
        public string BaseUrl { get; set; } = "https://accept.paymob.com/api";
    }
}