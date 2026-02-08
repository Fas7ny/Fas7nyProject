using Fas7ny.Application.DTOs.Payment.Request;
using Fas7ny.Application.ServivesInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;


    namespace Fas7ny.Application.Services.PaymentService
    {
        public class PaymobService : IPaymobService
        {
            private readonly HttpClient _httpClient;
            private readonly IConfiguration _configuration;
            private readonly ILogger<PaymobService> _logger;
            private readonly string _apiKey;
            private readonly string _integrationId;
            private readonly string _currency;

            public PaymobService(
                HttpClient httpClient,
                IConfiguration configuration,
                ILogger<PaymobService> logger)
            {
                _httpClient = httpClient;
                _configuration = configuration;
                _logger = logger;

                _apiKey = configuration["Paymob:ApiKey"]
                    ?? throw new ArgumentNullException("Paymob:ApiKey is missing");
                _integrationId = configuration["Paymob:IntegrationId"]
                    ?? throw new ArgumentNullException("Paymob:IntegrationId is missing");
                _currency = configuration["Paymob:Currency"] ?? "EGP";

                var baseUrl = configuration["Paymob:BaseUrl"] ?? "https://accept.paymob.com/api";
                _httpClient.BaseAddress = new Uri(baseUrl);
            }
            public async Task RefundAsync(string token, int orderId, decimal amount)
            {
                var response = await _httpClient.PostAsync(
                    "/acceptance/void_refund/refund",
                    new StringContent(
                        JsonSerializer.Serialize(new
                        {
                            auth_token = token,
                            order_id = orderId,
                            amount_cents = (int)(amount * 100)
                        }),
                        Encoding.UTF8,
                        "application/json"));

                if (!response.IsSuccessStatusCode)
                    throw new ApplicationException(await response.Content.ReadAsStringAsync());
            }

            public async Task<string> GetTokenAsync()
            {
                var response = await _httpClient.PostAsync(
                    "/auth/tokens",
                    new StringContent(
                        JsonSerializer.Serialize(new { api_key = _apiKey }),
                        Encoding.UTF8,
                        "application/json"));

                var body = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError(
                        "Paymob API Error | StatusCode: {StatusCode} | Response: {Body}",
                        response.StatusCode,
                        body
                    );
                    throw new ApplicationException(
                        $"Paymob API failed. StatusCode: {(int)response.StatusCode} - {response.StatusCode}. Response: {body}"
                    );
                }

                var result = JsonSerializer.Deserialize<PaymobTokenResponse>(body);
                return result?.Token
                       ?? throw new ApplicationException("Paymob token missing");
            }

            public async Task<int> CreateOrderAsync(string token, decimal amount)
            {
                var amountCents = (int)(amount * 100);

                var response = await _httpClient.PostAsync(
                    "/ecommerce/orders",
                    new StringContent(
                        JsonSerializer.Serialize(new
                        {
                            auth_token = token,
                            delivery_needed = false,
                            amount_cents = amountCents,
                            currency = _currency,
                            items = Array.Empty<object>()
                        }),
                        Encoding.UTF8,
                        "application/json"));

                var body = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new ApplicationException(body);

                var result = JsonSerializer.Deserialize<PaymobOrderResponse>(body);

                return result?.Id
                       ?? throw new ApplicationException("Paymob order id missing");
            }

            public async Task<string> GetPaymentKeyAsync(
                string token, int orderId, CreatePaymentRequest dto, decimal amount)
            {
                var amountCents = (int)(amount * 100);

                var response = await _httpClient.PostAsync(
                    "/acceptance/payment_keys",
                    new StringContent(
                        JsonSerializer.Serialize(new
                        {
                            auth_token = token,
                            amount_cents = amountCents.ToString(),
                            expiration = 3600,
                            order_id = orderId,
                            billing_data = new
                            {
                                apartment = "NA",
                                email = dto.Email ?? "user@example.com",
                                floor = "NA",
                                first_name = dto.FirstName ?? "User",
                                street = "NA",
                                building = "NA",
                                phone_number = dto.PhoneNumber ?? "+20100000000",
                                shipping_method = "NA",
                                postal_code = "NA",
                                city = "NA",
                                country = "NA",
                                last_name = dto.LastName ?? "Name",
                                state = "NA"
                            },
                            currency = _currency,
                            integration_id = _integrationId
                        }),
                        Encoding.UTF8,
                        "application/json"));

                var body = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError(
                        "Paymob Payment Key Error | StatusCode: {StatusCode} | Response: {Body}",
                        response.StatusCode,
                        body
                    );
                    throw new ApplicationException(
                        $"Paymob payment key generation failed. StatusCode: {(int)response.StatusCode} - {response.StatusCode}. Response: {body}"
                    );
                }

                var result = JsonSerializer.Deserialize<PaymobPaymentKeyResponse>(body);
                return result?.Token
                       ?? throw new ApplicationException("Paymob payment key missing");
            }

            #region Response Models

            private class PaymobTokenResponse
            {
                public string Token { get; set; }
            }

            private class PaymobOrderResponse
            {
                public int Id { get; set; }
            }

            private class PaymobPaymentKeyResponse
            {
                public string Token { get; set; }
            }

            #endregion
        }
    }


       
