using Fas7ny.Application.DTOs.Payment.Request;
using Fas7ny.Application.DTOs.Payment.Response;
using Fas7ny.Application.ServivesInterfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace Fas7ny.Application.Services.Payment
{
    public class PaymobService : IPaymobService
    {
        private readonly HttpClient _httpClient;
        private readonly PaymobOptions _options;

        public PaymobService(HttpClient httpClient, IOptions<PaymobOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        }

        public async Task<string> GetTokenAsync()
        {
            var requestBody = new { api_key = _options.ApiKey };

            var response = await _httpClient.PostAsJsonAsync("/auth/tokens", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Failed to get Paymob token. Status: {response.StatusCode}, Error: {errorContent}");
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(jsonString);

            var token = doc.RootElement.GetProperty("token").GetString();
            return token ?? throw new ApplicationException("Token was null in Paymob response");
        }

        public async Task<int> CreateOrderAsync(string token, decimal totalPrice)
        {
            var requestBody = new
            {
                auth_token = token,
                delivery_needed = false,
                amount_cents = (int)(totalPrice * 100),
                currency = _options.Currency,
                items = Array.Empty<object>()
            };

            var response = await _httpClient.PostAsJsonAsync("/Payment/CreatePayment", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Failed to create Paymob order. Status: {response.StatusCode}, Error: {errorContent}");
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(jsonString);

            return doc.RootElement.GetProperty("id").GetInt32();
        }

        public async Task<string> GetPaymentKeyAsync(
            string token,
            int orderId,
            CreatePaymentRequest paymentRequest,
            decimal totalPrice)
        {
            var requestBody = new
            {
                auth_token = token,
                amount_cents = (int)(totalPrice * 100),
                expiration = 3600, // Token expires in 1 hour
                order_id = orderId,
                currency = _options.Currency,
                integration_id = _options.IntegrationId,
                billing_data = new
                {
                    first_name = paymentRequest.FirstName ?? "User",
                    last_name = paymentRequest.LastName ?? "Customer",
                    email = paymentRequest.Email ?? "user@example.com",
                    phone_number = paymentRequest.PhoneNumber ?? "01000000000",
                    apartment = "NA",
                    floor = "NA",
                    street = "NA",
                    building = "NA",
                    shipping_method = "NA",
                    postal_code = "NA",
                    city = "NA",
                    country = "EGP",
                    state = "NA"
                }
            };

            var response = await _httpClient.PostAsJsonAsync("/acceptance/payment_keys", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Failed to get payment key. Status: {response.StatusCode}, Error: {errorContent}");
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(jsonString);

            var paymentKey = doc.RootElement.GetProperty("token").GetString();
            return paymentKey ?? throw new ApplicationException("Payment key was null in Paymob response");
        }

        public async Task<PaymentResponse> InitiatePaymentAsync(CreatePaymentRequest request)
        {
            // Step 1: Get authentication token
            var authToken = await GetTokenAsync();

            // Step 2: Create order
            var orderId = await CreateOrderAsync(authToken, request.Amount);

            // Step 3: Get payment key
            var paymentKey = await GetPaymentKeyAsync(authToken, orderId, request, request.Amount);

            // Step 4: Construct payment URL (for iframe/redirect)
            var paymentUrl = $"https://accept.paymob.com/api/acceptance/iframes/{_options.IntegrationId}?payment_token={paymentKey}";

            return new PaymentResponse
            {
                PaymentKey = paymentKey,
                OrderId = orderId,
                PaymentUrl = paymentUrl,
                Amount = request.Amount
            };
        }
    }
}