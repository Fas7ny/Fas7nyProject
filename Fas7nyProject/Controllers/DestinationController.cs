using Fas7ny.Application.DTOs.Ai.Request;
using Fas7ny.Application.DTOs.Destination.Request;
using Fas7ny.Application.ServivesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fas7nyProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/destinations")]
    public class DestinationController : ControllerBase
    {
        private readonly IMapboxSearchService _mapbox;
        private readonly IAiService _aiService;

        public DestinationController(
            IMapboxSearchService mapbox,
            IAiService aiService)
        {
            _mapbox = mapbox;
            _aiService = aiService;
        }


        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string query,
            [FromQuery] string? country = null,
            [FromQuery] string language = "en",
            [FromQuery] int limit = 5)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Search query is required");

            var request = new MapboxGeocodeRequest
            {
                Query = query,
                Country = country,
                Language = language,
                Limit = limit
            };

            var result = await _mapbox.SearchAsync(request);

            if (result?.Features == null || !result.Features.Any())
                return NotFound(new { message = "No locations found" });

            return Ok(result.Features.Select(f => new
            {
                name = f.Text,
                fullName = f.PlaceName,
                longitude = f.Center[0],
                latitude = f.Center[1]

            }));
        }


        [HttpGet("recommendations")]
        public async Task<IActionResult> GetRecommendations(

            [FromQuery] int count = 10,
            [FromQuery] decimal? budget = null,
            [FromQuery] string? travelStyle = null)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
             ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
            var request = new RecommendationRequestDTO
            {
                RecommendationType = "Destination",
                NumberOfRecommendations = count,
                Budget = budget,
                TravelStyle = travelStyle,
                UsePersonalizedData = !string.IsNullOrEmpty(userId)
            };

            var result = await _aiService.GetRecommendationsAsync(request);

            return Ok(result.Recommendations);
        }
    }
}
