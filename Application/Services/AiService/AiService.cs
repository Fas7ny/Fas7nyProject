using Fas7ny.Application.DTOs.Ai.Request;
using Fas7ny.Application.DTOs.Ai.Response;
using Fas7ny.Application.Options;
using Fas7ny.Application.ServivesInterfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using System.Text;
using System.Text.Json;

namespace Fas7ny.Application.Services.AiService
{
    public class AiService : IAiService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenRouterSettings _settings;
        private readonly ILogger<AiService> _logger;
        private readonly AsyncRetryPolicy _retryPolicy;

        public AiService(
     HttpClient httpClient,
     IOptions<OpenRouterSettings> settings,
     ILogger<AiService> logger)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _logger = logger;


            var timeoutSeconds = _settings.TimeoutSeconds > 0 ? _settings.TimeoutSeconds : 60;
            _httpClient.Timeout = TimeSpan.FromSeconds(timeoutSeconds);

            _retryPolicy = Policy
                .Handle<HttpRequestException>()
                .Or<TaskCanceledException>()
                .WaitAndRetryAsync(
                    3,
                    retry => TimeSpan.FromSeconds(Math.Pow(2, retry)),
                    (ex, _, retry, _) =>
                    {
                        _logger.LogWarning(
                            "Retry {Retry} due to {Message}",
                            retry, ex.Message);
                    });
        }
        #region Core

        private async Task<T> SendJsonPromptAsync<T>(
     string systemPrompt,
     string userPrompt,
     int maxTokens = 1024,
     double temperature = 0.3)
        {
            try
            {
                _logger.LogInformation("Sending request to OpenRouter for type {Type}", typeof(T).Name);

                var body = new
                {
                    model = _settings.DefaultModel,
                    messages = new[]
                    {
                new
                {
                    role = "system",
                    content = systemPrompt
                },
                new
                {
                    role = "user",
                    content = userPrompt
                }
            },
                    temperature,
                    max_tokens = maxTokens,
                    response_format = new { type = "json_object" }
                };

                // Use absolute URL
                var absoluteUrl = $"{_settings.BaseUrl}/chat/completions";

                var request = new HttpRequestMessage(
                    HttpMethod.Post,
                    absoluteUrl) // Changed from relative to absolute URL
                {
                    Content = new StringContent(
                        JsonSerializer.Serialize(body),
                        Encoding.UTF8,
                        "application/json")
                };

                request.Headers.Add("Authorization", $"Bearer {_settings.ApiKey}");
                request.Headers.Add("HTTP-Referer", "https://fas7ny.com");
                request.Headers.Add("X-Title", "Fas7ny Tourism Platform");

                var response = await _retryPolicy.ExecuteAsync(async () =>
                {
                    var res = await _httpClient.SendAsync(request);

                    if ((int)res.StatusCode == 429)
                    {
                        _logger.LogWarning("Rate limit hit, retrying...");
                        throw new HttpRequestException("Rate limit");
                    }

                    return res;
                });

                var raw = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("OpenRouter API error: {StatusCode} - {Response}",
                        (int)response.StatusCode, raw);
                    throw new ApplicationException($"OpenRouter API error: {raw}");
                }

                var openRouterResponse = JsonSerializer.Deserialize<OpenRouterResponse>(raw);

                if (openRouterResponse?.Choices == null || openRouterResponse.Choices.Length == 0)
                {
                    throw new ApplicationException("No choices in OpenRouter response");
                }

                var text = openRouterResponse.Choices[0].Message.Content;

                if (string.IsNullOrWhiteSpace(text))
                {
                    throw new ApplicationException("Empty AI response");
                }

                _logger.LogDebug("AI Response: {Response}", text);

                var cleanedText = CleanJsonResponse(text);

                var result = JsonSerializer.Deserialize<T>(
                    cleanedText,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        AllowTrailingCommas = true
                    });

                if (result == null)
                {
                    throw new ApplicationException("Failed to deserialize response");
                }

                _logger.LogInformation("Successfully processed OpenRouter response for type {Type}", typeof(T).Name);

                return result;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON parsing error");
                throw new ApplicationException("Invalid JSON response from AI", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SendJsonPromptAsync");
                throw;
            }
        }

        private static string CleanJsonResponse(string content)
        {
            content = content.Trim();

            if (content.StartsWith("```json", StringComparison.OrdinalIgnoreCase))
                content = content[7..];
            else if (content.StartsWith("```"))
                content = content[3..];

            if (content.EndsWith("```"))
                content = content[..^3];

            content = content.Trim();

            var jsonStart = content.IndexOf('{');
            var jsonEnd = content.LastIndexOf('}');

            if (jsonStart >= 0 && jsonEnd >= jsonStart)
            {
                content = content.Substring(jsonStart, jsonEnd - jsonStart + 1);
            }

            return content;
        }

        #endregion

        #region Features

        public Task<UserBehaviorAnalysisResponseDTO> AnalyzeUserBehaviorAsync(
            UserBehaviorAnalysisRequestDTO query)
        {
            var systemPrompt = """
                You are an AI assistant specialized in analyzing user behavior in tourism applications.
                Analyze patterns, preferences, and provide recommendations.
                Return ONLY valid JSON matching the exact structure requested.
                """;

            var userPrompt = $$"""
                Analyze user behavior and return JSON with this structure:
                {
                  "success": true,
                  "userId": "{{query.UserId}}",
                  "profile": {
                    "travelPersonality": "Adventure Seeker",
                    "preferredDestinations": ["Beach", "Mountains"],
                    "preferredActivities": ["Hiking", "Swimming"],
                    "averageBudget": 1500,
                    "averageTripDuration": 7,
                    "preferredSeason": "Summer",
                    "accommodationPreference": "Hotel",
                    "interactionPatterns": {}
                  },
                  "travelPatterns": [
                    {
                      "patternType": "Frequency",
                      "description": "Travels 2-3 times per year",
                      "confidence": 0.85,
                      "supportingEvidence": ["Booking history", "Search patterns"]
                    }
                  ],
                  "predictions": [
                    {
                      "predictionType": "Next Trip",
                      "description": "Likely to book beach vacation",
                      "probability": 0.75,
                      "factors": ["Past preferences", "Season"]
                    }
                  ],
                  "recommendations": [
                    {
                      "itemType": "Destination",
                      "itemId": 1,
                      "name": "Maldives",
                      "description": "Perfect beach destination",
                      "matchScore": 0.92,
                      "reasonForRecommendation": "Matches beach preference"
                    }
                  ],
                  "analyzedAt": "{{DateTime.UtcNow:O}}"
                }

                User Data: {{JsonSerializer.Serialize(query)}}
                """;

            return SendJsonPromptAsync<UserBehaviorAnalysisResponseDTO>(
                systemPrompt,
                userPrompt,
                2048,
                0.4);
        }

        public Task<AiChatResponseDTO> ChatAsync(AiChatRequestDto query)
        {
            var systemPrompt = """
                You are Fas7ny AI, a helpful tourism assistant.
                Provide friendly, accurate travel information.
                Return ONLY valid JSON.
                """;

            var userPrompt = $$"""
                Respond with this JSON structure:
                {
                  "success": true,
                  "message": "Response generated",
                  "response": "Your helpful response here",
                  "suggestedActions": [
                    {
                      "actionType": "ViewPackages",
                      "label": "Browse Packages",
                      "description": "Explore available packages",
                      "parameters": {}
                    }
                  ],
                  "relatedRecommendations": [],
                  "timestamp": "{{DateTime.UtcNow:O}}",
                  "conversationId": "{{Guid.NewGuid()}}"
                }

                User Message: {{query.Message}}
                Context: {{query.ContextType ?? "General"}}
                """;

            return SendJsonPromptAsync<AiChatResponseDTO>(
                systemPrompt,
                userPrompt,
                1024,
                0.7);
        }

        public Task<GeneratedPackageResponse> GeneratePackageAsync(
     GeneratePackageRequestDTO query)
        {
            var systemPrompt = """
        You are an expert travel planner.
        Create realistic, detailed travel packages.
        Return ONLY valid JSON matching the structure requested.
        """;

            var pricePerNight = (query.Budget / query.DurationDays / query.NumberOfTravelers) * 0.4m;
            var totalHotelCost = pricePerNight * query.DurationDays;
            var pricePerPerson = query.Budget / query.NumberOfTravelers;

            var userPrompt = $$"""
        Generate a travel package with this structure:
        {
          "success": true,
          "message": "Package generated successfully",
          "data": {
            "packageName": "{{query.DestinationCity}} {{query.TravelStyle}} Adventure",
            "description": "A comprehensive travel package description in one paragraph",
            "durationDays": {{query.DurationDays}},
            "totalPrice": {{query.Budget}},
            "pricePerPerson": {{pricePerPerson}},
            "recommendedHotel": {
              "hotelName": "Sample Hotel",
              "address": "City Center",
              "rating": 4,
              "pricePerNight": {{pricePerNight}},
              "totalHotelCost": {{totalHotelCost}},
              "amenities": ["WiFi", "Pool", "Breakfast"],
              "description": "Comfortable hotel in prime location",
              "roomType": "Deluxe"
            },
            "dailyItinerary": [
              {
                "dayNumber": 1,
                "title": "Arrival Day",
                "description": "Check-in and city exploration",
                "activities": [],
                "morningActivity": "Hotel check-in",
                "afternoonActivity": "City tour",
                "eveningActivity": "Welcome dinner",
                "mealSuggestions": ["Local cuisine"]
              }
            ],
            "recommendedActivities": [
              {
                "name": "City Tour",
                "description": "Guided tour of main attractions",
                "entryFee": 50,
                "openingHours": "9 AM - 6 PM",
                "recommendedDuration": 4,
                "category": "Sightseeing",
                "rating": 4.5,
                "whyRecommended": "Popular attraction"
              }
            ],
            "recommendedRestaurants": [
              {
                "name": "Local Restaurant",
                "cuisine": "Local",
                "priceRange": "$$",
                "rating": 4.3,
                "description": "Authentic local food",
                "averageCostPerPerson": 30,
                "whyRecommended": "Highly rated"
              }
            ],
            "travelTips": ["Bring sunscreen", "Book activities early"],
            "bestTimeToVisit": "Year-round"
          },
          "generatedAt": "{{DateTime.UtcNow:O}}",
          "generationId": "{{Guid.NewGuid()}}"
        }

        Requirements:
        - Destination: {{query.DestinationCity}}
        - Duration: {{query.DurationDays}} days
        - Budget: ${{query.Budget}} for {{query.NumberOfTravelers}} people
        - Style: {{query.TravelStyle}}
        - Activities: {{string.Join(", ", query.PreferredActivities.Take(3))}}
        """;

            return SendJsonPromptAsync<GeneratedPackageResponse>(
                systemPrompt,
                userPrompt,
                3072,
                0.5);
        }

        public Task<RecommendationResponseDTO> GetRecommendationsAsync(
            RecommendationRequestDTO query)
        {
            var systemPrompt = """
                You are a tourism recommendation engine.
                Provide personalized travel recommendations.
                Return ONLY valid JSON.
                """;

            var userPrompt = $$"""
                Generate {{query.NumberOfRecommendations}} recommendations with this structure:
                {
                  "success": true,
                  "message": "Recommendations generated",
                  "recommendations": [
                    {
                      "itemType": "{{query.RecommendationType}}",
                      "itemId": 1,
                      "name": "Sample Destination",
                      "description": "Amazing travel destination",
                      "imageUrl": "https://example.com/image.jpg",
                      "matchScore": 0.95,
                      "rating": 4.5,
                      "price": {{query.Budget ?? 1000}},
                      "location": "City, Country",
                      "tags": ["Beach", "Adventure"],
                      "reasonForRecommendation": "Matches your preferences",
                      "additionalData": {}
                    }
                  ],
                  "metadata": {
                    "algorithmUsed": "AI-Powered",
                    "basedOn": "User preferences",
                    "totalItemsAnalyzed": 100,
                    "averageMatchScore": 0.85,
                    "consideredFactors": ["Budget", "Style", "Duration"]
                  },
                  "generatedAt": "{{DateTime.UtcNow:O}}"
                }

                Criteria:
                - Type: {{query.RecommendationType}}
                - Count: {{query.NumberOfRecommendations}}
                - Budget: {{query.Budget}}
                - Duration: {{query.DurationDays}}
                - Style: {{query.TravelStyle}}
                - Preferences: {{string.Join(", ", query.Preferences.Take(5))}}
                """;

            return SendJsonPromptAsync<RecommendationResponseDTO>(
                systemPrompt,
                userPrompt,
                2048,
                0.6);
        }

        #endregion

        #region Response Models

        private class OpenRouterResponse
        {
            public Choice[] Choices { get; set; } = Array.Empty<Choice>();
        }

        private class Choice
        {
            public Message Message { get; set; } = new();
        }

        private class Message
        {
            public string Content { get; set; } = string.Empty;
        }

        #endregion
    }
}
