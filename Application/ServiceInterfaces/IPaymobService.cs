using Fas7ny.Application.DTOs.Payment.Request;
using Fas7ny.Application.DTOs.Payment.Response;

namespace Fas7ny.Application.ServivesInterfaces
{
    public interface IPaymobService
    {
        Task<string> GetTokenAsync();
        Task<int> CreateOrderAsync(string token, decimal totalPrice);
        Task<string> GetPaymentKeyAsync(string token, int orderId, CreatePaymentRequest paymentRequest, decimal totalPrice);
        Task<PaymentResponse> InitiatePaymentAsync(CreatePaymentRequest request);
    }
}
