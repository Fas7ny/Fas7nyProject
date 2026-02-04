using Fas7ny.Application.DTOs.Ai.Request;
using Fas7ny.Application.DTOs.Ai.Response;
using Fas7ny.Application.ServivesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fas7nyProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly IAiService _service;
        private readonly ILogger<AiController> _logger;

        public AiController(IAiService aiService, ILogger<AiController> logger)
        {
            _logger = logger;
            _service = aiService;
        }

        #region User Behavior Analysis

        [HttpPost("AnalyzeUserBehavior")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserBehaviorAnalysisResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAnalyzeUserBehavior(
            [FromBody] UserBehaviorAnalysisRequestDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _service.AnalyzeUserBehaviorAsync(dto);

                if (result == null)
                {
                    return NotFound(new { message = $"No behavior data found for user {dto.UserId}" });
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid request for user behavior analysis");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "AnalyzeUserBehavior failed | UserId: {UserId}",
                    dto.UserId);

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while analyzing user behavior" }
                );
            }


        }

        [HttpGet("AnalyzeUserBehavior")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<UserBehaviorAnalysisResponseDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAnalyzeUserBehavior(
            [FromQuery] string? userId = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                // This would typically fetch from a database of past analyses
                // For now, return a placeholder response
                _logger.LogInformation("Fetching user behavior analyses. UserId: {UserId}, Page: {Page}",
                    userId, page);

                // TODO: Implement retrieval from database/cache
                return Ok(new
                {
                    message = "User behavior analysis history",
                    page,
                    pageSize,
                    data = new List<UserBehaviorAnalysisResponseDTO>()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user behavior analyses");
                return StatusCode(500, new { message = "An error occurred while retrieving analyses" });
            }
        }

        [HttpGet("{userId}/behavior-summary")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserBehaviorAnalysisResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserBehaviorSummary(
            string userId,
            [FromQuery] int days = 30)
        {
            try
            {
                var request = new UserBehaviorAnalysisRequestDTO
                {
                    UserId = userId,
                    AnalysisPeriodDays = days,
                    IncludePredictions = true,
                    IncludeRecommendations = true
                };

                var result = await _service.AnalyzeUserBehaviorAsync(request);

                if (result == null)
                {
                    return NotFound(new { message = $"User {userId} not found" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving behavior summary for user {UserId}", userId);
                return StatusCode(500, new { message = "An error occurred while retrieving behavior summary" });
            }
        }

        [HttpPost("batch-analyze")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<UserBehaviorAnalysisResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BatchAnalyzeUserBehavior(
            [FromBody] List<UserBehaviorAnalysisRequestDTO> requests)
        {
            try
            {
                if (requests == null || !requests.Any())
                {
                    return BadRequest(new { message = "Request list cannot be empty" });
                }

                if (requests.Count > 100)
                {
                    return BadRequest(new { message = "Maximum 100 users can be analyzed at once" });
                }

                var tasks = requests.Select(r => _service.AnalyzeUserBehaviorAsync(r));
                var results = await Task.WhenAll(tasks);

                return Ok(results.Where(r => r != null));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in batch analysis");
                return StatusCode(500, new { message = "An error occurred during batch analysis" });
            }
        }

        #endregion

        #region Chat

        [AllowAnonymous]
        [HttpPost("ChatWithAI")]
        [ProducesResponseType(typeof(AiChatResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChatWithAI([FromBody] AiChatRequestDto userQuery)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _service.ChatAsync(userQuery);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ChatWithAI for user {UserId}", userQuery?.UserId);
                return StatusCode(500, new { message = "An error occurred while processing your message" });
            }
        }

        [AllowAnonymous]
        [HttpGet("chat")]
        [ProducesResponseType(typeof(List<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetChat(
            [FromQuery] string? userId = null,
            [FromQuery] string? conversationId = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            try
            {
                // TODO: Implement chat history retrieval from database
                _logger.LogInformation("Fetching chat history. UserId: {UserId}, ConversationId: {ConversationId}",
                    userId, conversationId);

                return Ok(new
                {
                    message = "Chat history",
                    page,
                    pageSize,
                    data = new List<object>() // Replace with actual chat history
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving chat history");
                return StatusCode(500, new { message = "An error occurred while retrieving chat history" });
            }
        }

        #endregion

        #region Package Generation

        [HttpPost("generate-package")]
        [Authorize]
        [ProducesResponseType(typeof(GeneratedPackageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GeneratePackage([FromBody] GeneratePackageRequestDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _logger.LogInformation("Generating package for user {UserId} to {Destination}",
                    dto.UserId, dto.DestinationCity);

                var result = await _service.GeneratePackageAsync(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating package for user {UserId}", dto?.UserId);

                return StatusCode(500, new
                {
                    message = "An error occurred while generating the package",
                    error = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }

        }

        [HttpPost("regenerate-package")]
        [Authorize]
        [ProducesResponseType(typeof(GeneratedPackageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegeneratePackage([FromBody] RegeneratePackageRequestDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _logger.LogInformation("Regenerating package {PackageId} for user {UserId}",
                    dto.PackageId, dto.UserId);

                // TODO: Fetch original package data
                // TODO: Apply modifications based on feedback
                // TODO: Call GeneratePackageAsync with updated parameters

                return Ok(new
                {
                    success = true,
                    message = "Package regenerated successfully",
                    packageId = dto.PackageId
                    // Include regenerated package data
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error regenerating package {PackageId}", dto?.PackageId);
                return StatusCode(500, new { message = "An error occurred while regenerating the package" });
            }
        }

        [HttpPost("save-generated-package")]
        [Authorize]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveGeneratedPackage([FromBody] SaveGeneratedPackageRequestDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _logger.LogInformation("Saving generated package '{PackageName}' for user {UserId}",
                    dto.PackageName, dto.UserId);

                // TODO: Save package to database
                // TODO: Create associated records (hotels, activities, itinerary)

                var packageId = Guid.NewGuid(); // Replace with actual saved package ID

                return CreatedAtAction(
                    nameof(GetPackageById),
                    new { id = packageId },
                    new
                    {
                        success = true,
                        message = "Package saved successfully",
                        packageId,
                        packageName = dto.PackageName
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving generated package for user {UserId}", dto?.UserId);
                return StatusCode(500, new { message = "An error occurred while saving the package" });
            }
        }

        [HttpGet("packages/{id}")]
        [Authorize]
        public async Task<IActionResult> GetPackageById(Guid id)
        {
            // TODO: Implement package retrieval
            return Ok(new { id, message = "Package details" });
        }

        #endregion

        #region Recommendations

        [AllowAnonymous]
        [HttpGet("get-recommendations")]
        [ProducesResponseType(typeof(RecommendationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecommendations(
            [FromQuery] string? userId = null,
            [FromQuery] string type = "Destination",
            [FromQuery] int count = 10,
            [FromQuery] decimal? budget = null,
            [FromQuery] int? duration = null,
            [FromQuery] string? travelStyle = null,
            [FromQuery] string? destination = null)
        {
            try
            {
                var request = new RecommendationRequestDTO
                {
                    UserId = userId ?? "anonymous",
                    RecommendationType = type,
                    NumberOfRecommendations = count,
                    Budget = budget,
                    DurationDays = duration,
                    TravelStyle = travelStyle,
                    Destination = destination,
                    UsePersonalizedData = !string.IsNullOrEmpty(userId)
                };

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _logger.LogInformation("Getting {Type} recommendations for user {UserId}",
                    type, userId ?? "anonymous");

                var result = await _service.GetRecommendationsAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting recommendations");
                return StatusCode(500, new { message = "An error occurred while getting recommendations" });
            }
        }

        [AllowAnonymous]
        [HttpPost("get-recommendations")]
        [ProducesResponseType(typeof(RecommendationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRecommendationsPost([FromBody] RecommendationRequestDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _logger.LogInformation("Getting {Type} recommendations for user {UserId}",
                    dto.RecommendationType, dto.UserId);

                var result = await _service.GetRecommendationsAsync(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting recommendations");
                return StatusCode(500, new { message = "An error occurred while getting recommendations" });
            }
        }

        [AllowAnonymous]
        [HttpPost("similar-recommendations")]
        [ProducesResponseType(typeof(RecommendationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSimilarRecommendations([FromBody] SimilarItemRecommendationRequestDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _logger.LogInformation("Getting similar {ItemType} recommendations for item {ItemId}",
                    dto.ItemType, dto.ItemId);

                // TODO: Implement similar item recommendations
                // This would analyze the item and find similar items based on characteristics

                return Ok(new RecommendationResponseDTO
                {
                    Success = true,
                    Message = $"Similar {dto.ItemType} recommendations",
                    Recommendations = new List<RecommendationItem>(),
                    GeneratedAt = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting similar recommendations");
                return StatusCode(500, new { message = "An error occurred while getting similar recommendations" });
            }
        }

        #endregion

        #region Health Check

        [AllowAnonymous]
        [HttpGet("health")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult HealthCheck()
        {
            return Ok(new
            {
                status = "healthy",
                service = "AI Service",
                timestamp = DateTime.UtcNow
            });
        }

        #endregion
    }
}