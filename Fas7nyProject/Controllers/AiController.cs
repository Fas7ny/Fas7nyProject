using Fas7ny.Application.DTOs.Ai.Request;
using Fas7ny.Application.ServivesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fas7nyProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly IAiService _aiService;
        private readonly ILogger<AiController> _logger;

        public AiController(IAiService aiService, ILogger<AiController> logger)
        {
            _aiService = aiService;
            _logger = logger;
        }

        #region Helpers

        private string? GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
        }

        #endregion

        #region User Behavior

        [HttpPost("analyze-user-behavior")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AnalyzeUserBehavior(
            [FromBody] UserBehaviorAnalysisRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var adminId = GetCurrentUserId();

            _logger.LogInformation(
                "Admin {AdminId} analyzing user behavior",
                adminId
            );

            var result = await _aiService.AnalyzeUserBehaviorAsync(dto);

            return result == null
                ? NotFound()
                : Ok(result);
        }

        [HttpGet("behavior-summary")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetUserBehaviorSummary(
            [FromQuery] int days = 30)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
                return Unauthorized();

            var request = new UserBehaviorAnalysisRequestDTO
            {
                AnalysisPeriodDays = days,
                IncludePredictions = true,
                IncludeRecommendations = true
            };

            _logger.LogInformation(
                "Fetching behavior summary for user {UserId}",
                userId
            );

            var result = await _aiService.AnalyzeUserBehaviorAsync(request);

            return result == null
                ? NotFound()
                : Ok(result);
        }

        #endregion

        #region Chat

        [AllowAnonymous]
        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] AiChatRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetCurrentUserId() ?? "anonymous";

            _logger.LogInformation(
                "Chat request from {UserId}",
                userId
            );

            var result = await _aiService.ChatAsync(dto);
            return Ok(result);
        }

        #endregion

        #region Package Generation

        [Authorize(Roles = "Admin")]
        [HttpPost("generate-package")]
        public async Task<IActionResult> GeneratePackage(
            [FromBody] GeneratePackageRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetCurrentUserId();
            if (userId == null)
                return Unauthorized();

            _logger.LogInformation(
                "Generating package for user {UserId}",
                userId
            );

            var result = await _aiService.GeneratePackageAsync(dto);
            return Ok(result);
        }

        #endregion

        #region Recommendations

        [AllowAnonymous]
        [HttpGet("recommendations")]
        public async Task<IActionResult> GetRecommendations(
            [FromQuery] string type = "Destination",
            [FromQuery] int count = 10)
        {
            var userId = GetCurrentUserId();

            var request = new RecommendationRequestDTO
            {
                RecommendationType = type,
                NumberOfRecommendations = count,
                UsePersonalizedData = userId != null
            };

            var result = await _aiService.GetRecommendationsAsync(request);
            return Ok(result);
        }

        #endregion

        #region Health

        [AllowAnonymous]
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                status = "healthy",
                service = "Fas7ny AI",
                timestamp = DateTime.UtcNow
            });
        }

        #endregion
    }
}
