using Fas7ny.Application.DTOs.Payment.Request;

namespace Fas7ny.Application.ServivesInterfaces
{
    public interface IPaymobService
    {
        Task<string> GetTokenAsync();
        Task<int> CreateOrderAsync(string token, decimal amount);
        Task<string> GetPaymentKeyAsync(string token, int orderId, CreatePaymentRequest dto, decimal amount);
        Task RefundAsync(string token, int orderId, decimal amount);

    }
}
