using Fas7ny.Application.DTOs.Ai.Request;
using Fas7ny.Application.DTOs.Ai.Response;

namespace Fas7ny.Application.ServivesInterfaces
{


    public interface IAiService
    {
        Task<UserBehaviorAnalysisResponseDTO> AnalyzeUserBehaviorAsync(
            UserBehaviorAnalysisRequestDTO query);

        Task<AiChatResponseDTO> ChatAsync(AiChatRequestDto query);

        Task<GeneratedPackageResponse> GeneratePackageAsync(
            GeneratePackageRequestDTO query);

        Task<RecommendationResponseDTO> GetRecommendationsAsync(
            RecommendationRequestDTO query);
    }




}
