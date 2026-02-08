using Fas7ny.Application.DTOs.Ai.Request;
using Fas7ny.Application.DTOs.Ai.Response;
using Fas7ny.Application.Options;
using Fas7ny.Application.ServivesInterfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Fas7ny.Application.Services.AiService
{
    public class AiService : IAiService
    {
        private readonly HttpClient _httpClient;
        private readonly GeminiSettings _settings;
        private readonly ILogger<AiService> _logger;

        public AiService(
            HttpClient httpClient,
            IOptions<GeminiSettings> settings,
            ILogger<AiService> logger)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _logger = logger;

            if (string.IsNullOrWhiteSpace(_settings.BaseUrl))
                throw new ApplicationException("Gemini BaseUrl is missing");

            if (string.IsNullOrWhiteSpace(_settings.ApiKey))
                throw new ApplicationException("Gemini ApiKey is missing");

            _httpClient.Timeout =
                TimeSpan.FromSeconds(
                    _settings.TimeoutSeconds > 0
                        ? _settings.TimeoutSeconds
                        : 60
                );
        }

        private async Task<T> SendAsync<T>(
            string prompt,
            object responseSchema,
            int maxTokens,
            double temperature)
        {
            var body = new
            {
                contents = new[]
                {
                    new
                    {
                        role = "user",
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                },
                generationConfig = new
                {
                    temperature,
                    maxOutputTokens = maxTokens,
                    responseMimeType = "application/json",
                    responseSchema
                }
            };

            var url =
                $"{_settings.BaseUrl.TrimEnd('/')}/models/{_settings.Model}:generateContent?key={_settings.ApiKey}";

            var response = await _httpClient.PostAsync(
                url,
                new StringContent(
                    JsonSerializer.Serialize(body),
                    Encoding.UTF8,
                    "application/json"));

            var raw = await response.Content.ReadAsStringAsync();
            if (raw.Contains("RESOURCE_EXHAUSTED"))
            {
                throw new ApplicationException(
                    "AI quota exceeded. Please retry later."
                );
            }

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(raw);

            var jsonStart = raw.IndexOf('{');
            var jsonEnd = raw.LastIndexOf('}');

            if (jsonStart < 0 || jsonEnd < jsonStart)
                throw new ApplicationException("Invalid JSON response");

            var json = raw[jsonStart..(jsonEnd + 1)];

            return JsonSerializer.Deserialize<T>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
        }

        public Task<UserBehaviorAnalysisResponseDTO>
            AnalyzeUserBehaviorAsync(UserBehaviorAnalysisRequestDTO query)
        {
            var prompt = JsonSerializer.Serialize(query);

            return SendAsync<UserBehaviorAnalysisResponseDTO>(
                prompt,
                GeminiSchemas.UserBehaviorAnalysis,
                2048,
                0.4);
        }

        public Task<AiChatResponseDTO> ChatAsync(AiChatRequestDto query)
        {
            return SendAsync<AiChatResponseDTO>(
                query.Message,
                GeminiSchemas.Chat,
                1024,
                0.7);
        }

        public Task<GeneratedPackageResponse>
            GeneratePackageAsync(GeneratePackageRequestDTO query)
        {
            var prompt = JsonSerializer.Serialize(query);

            return SendAsync<GeneratedPackageResponse>(
                prompt,
                GeminiSchemas.GeneratePackage,
                3072,
                0.5);
        }

        public Task<RecommendationResponseDTO>
            GetRecommendationsAsync(RecommendationRequestDTO query)
        {
            var prompt = JsonSerializer.Serialize(query);

            return SendAsync<RecommendationResponseDTO>(
                prompt,
                GeminiSchemas.Recommendations,
                2048,
                0.6);
        }
    }
}
