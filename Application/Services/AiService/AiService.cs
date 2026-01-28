using Fas7ny.Application.DTOs.Ai.Request;
using Fas7ny.Application.DTOs.Ai.Response;
using Fas7ny.Application.Options;
using Fas7ny.Application.ServivesInterfaces;
using Microsoft.Extensions.Options;
using OpenAI;
using System.ClientModel;
using System.Text.Json;
using OpenAIChatMessage = OpenAI.Chat.ChatMessage;
using SystemChatMessage = OpenAI.Chat.SystemChatMessage;
using UserChatMessage = OpenAI.Chat.UserChatMessage;

namespace Fas7ny.Application.Services.OpenAiService
{
    public class AiService : IAiService
    {
        private readonly OpenAIClient _openAiClient;
        private readonly OpenAIOptions _options;

        public AiService(IOptions<OpenAIOptions> options)
        {
            _options = options.Value;
            _openAiClient = new OpenAIClient(
                new ApiKeyCredential(_options.ApiKey)
            );
        }
        private static string CleanJsonResponse(string content)
        {
            content = content.Trim();

            if (content.StartsWith("```json"))
                content = content[7..];
            else if (content.StartsWith("```"))
                content = content[3..];

            if (content.EndsWith("```"))
                content = content[..^3];

            return content.Trim();
        }
        private async Task<T> SendJsonPromptAsync<T>(
            string systemPrompt,
            string userPrompt)
        {
            var chatClient = _openAiClient.GetChatClient(_options.Model);

            var messages = new List<OpenAIChatMessage>
            {
                new SystemChatMessage(systemPrompt),
                new UserChatMessage(userPrompt)
            };

            var response = await chatClient.CompleteChatAsync(messages);
            var content = response?.Value?.Content?.FirstOrDefault()?.Text;

            if (string.IsNullOrWhiteSpace(content))
                throw new ApplicationException("AI response was empty.");

            content = CleanJsonResponse(content);

            try
            {
                var result = JsonSerializer.Deserialize<T>(
                    content,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return result ?? throw new ApplicationException("Failed to parse AI response.");
            }
            catch (JsonException ex)
            {
                throw new ApplicationException(
                    $"AI response is not valid JSON. Response: {content}",
                    ex
                );
            }
        }


        public Task<UserBehaviorAnalysisResponseDTO> AnalyzeUserBehaviorAsync(
            UserBehaviorAnalysisRequestDTO query)
        {
            var systemPrompt = """
                You are an AI assistant specialized in analyzing user behavior
                in tourism applications. Return ONLY valid JSON. No explanations.
                """;

            var userData = new
            {
                query.UserId,

            };

            var userPrompt = $$"""
                Analyze the following user behavior data and return JSON ONLY
                with this exact structure:

                {
                  "behaviorPatterns": [],
                  "preferences": [],
                  "recommendations": [],
                  "riskFactors": []
                }

                User Data:
                {{JsonSerializer.Serialize(userData)}}
                """;

            return SendJsonPromptAsync<UserBehaviorAnalysisResponseDTO>(
                systemPrompt,
                userPrompt
            );
        }


        public async Task<AiChatResponseDTO> ChatAsync(AiChatRequestDto query)
        {
            var chatClient = _openAiClient.GetChatClient(_options.Model);

            var messages = new List<OpenAIChatMessage>
            {
                new SystemChatMessage("You are a helpful tourism assistant."),
                new UserChatMessage(query.Message ?? string.Empty)
            };

            var response = await chatClient.CompleteChatAsync(messages);
            var content = response?.Value?.Content?.FirstOrDefault()?.Text;

            if (string.IsNullOrWhiteSpace(content))
                throw new ApplicationException("AI response was empty.");

            return new AiChatResponseDTO
            {
                Response = content
            };
        }


        public Task<GeneratedPackageResponse> GeneratePackageAsync(
            GeneratePackageRequestDTO query)
        {
            var systemPrompt = """
                You are a professional travel planner.
                Return ONLY valid JSON.
                """;

            var userPrompt = $$"""
                Generate a travel package with this structure:

                {
                  "packageName": "string",
                  "description": "string",
                  "totalCost": 0.0
                }

                User Input:
                {{JsonSerializer.Serialize(query)}}
                """;

            return SendJsonPromptAsync<GeneratedPackageResponse>(
                systemPrompt,
                userPrompt
            );
        }


        public Task<RecommendationResponseDTO> GetRecommendationsAsync(
            RecommendationRequestDTO query)
        {
            var systemPrompt = """
                You are a tourism recommendation engine.
                Return ONLY valid JSON.
                """;

            var userPrompt = $$"""
                Provide recommendations using this structure:

                {
                  "recommendations": [
                    {
                      "title": "string",
                      "description": "string",
                      "relevanceScore": 0.95
                    }
                  ]
                }

                Preferences:
                {{JsonSerializer.Serialize(query.Preferences)}}
                """;

            return SendJsonPromptAsync<RecommendationResponseDTO>(
                systemPrompt,
                userPrompt
            );
        }





    }
}
