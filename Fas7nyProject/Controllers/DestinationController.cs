using Fas7ny.Application.DTOs.Ai.Request;
using Fas7ny.Application.DTOs.Destination.Request;
using Fas7ny.Application.DTOs.Destination.Response;
using Fas7ny.Application.ServivesInterfaces;
using Fas7ny.Domain.Entities;
using Fas7ny.Domain.RepoInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Fas7nyProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DestinationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapboxSearchService _mapbox;
        private readonly IAiService _aiService;
        private readonly ILogger<DestinationController> _logger;

        public DestinationController(
            IUnitOfWork unitOfWork,
            IMapboxSearchService mapbox,
            IAiService aiService,
            ILogger<DestinationController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapbox = mapbox;
            _aiService = aiService;
            _logger = logger;
        }

        #region CRUD

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int? cityId = null,
            [FromQuery] decimal? minRating = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var destinations = await _unitOfWork.Destinations.GetAllAsync();

            if (cityId.HasValue)
                destinations = destinations.Where(d => d.CityId == cityId.Value);

            if (minRating.HasValue)
                destinations = destinations.Where(d => d.Rating >= minRating.Value);

            var totalCount = destinations.Count();

            var data = destinations
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(MapToDto)
                .ToList();

            return Ok(new
            {
                data,
                page,
                pageSize,
                totalCount,
                totalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var destination = await _unitOfWork.Destinations.GetByIdAsync(id);
            if (destination == null)
                return NotFound(new { message = "Destination not found" });

            return Ok(MapToDto(destination));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateDestinationDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var city = await _unitOfWork.Cities.GetByIdAsync(dto.CityId);
            if (city == null)
                return BadRequest("City does not exist");

            var destination = new Destination
            {
                Name = dto.Name,
                CityId = dto.CityId,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                Rating = dto.Rating
            };

            await _unitOfWork.Destinations.AddAsync(destination);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = destination.Id },
                MapToDto(destination));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateDestinationDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            var destination = await _unitOfWork.Destinations.GetByIdAsync(id);
            if (destination == null)
                return NotFound("Destination not found");

            var city = await _unitOfWork.Cities.GetByIdAsync(dto.CityId);
            if (city == null)
                return BadRequest("City does not exist");

            destination.Name = dto.Name;
            destination.CityId = dto.CityId;
            destination.Description = dto.Description;
            destination.ImageUrl = dto.ImageUrl;
            destination.Rating = dto.Rating;

            await _unitOfWork.SaveChangesAsync();

            return Ok(MapToDto(destination));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var destination = await _unitOfWork.Destinations.GetByIdAsync(id);
            if (destination == null)
                return NotFound();

            await _unitOfWork.Destinations.DeleteAsync(destination);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Search

        [HttpGet("search")]
        public async Task<IActionResult> SearchMapbox(
            [FromQuery] string query,
            [FromQuery] string? country,
            [FromQuery] string? language = "en",
            [FromQuery] int limit = 5)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query is required");

            var request = new MapboxGeocodeRequest
            {
                Query = query,
                Country = country,
                Language = language,
                Limit = limit
            };

            var result = await _mapbox.SearchAsync(request);

            if (result?.Features == null || !result.Features.Any())
                return NotFound("No results found");

            return Ok(result);
        }

        #endregion

        #region AI Recommendations

        [HttpGet("recommendations")]
        public async Task<IActionResult> GetRecommendations(
            [FromQuery] string? userId,
            [FromQuery] int count = 10,
            [FromQuery] decimal? budget = null,
            [FromQuery] string? travelStyle = null)
        {
            var request = new RecommendationRequestDTO
            {
                UserId = userId ?? "anonymous",
                RecommendationType = "Destination",
                NumberOfRecommendations = count,
                Budget = budget,
                TravelStyle = travelStyle,
                UsePersonalizedData = !string.IsNullOrEmpty(userId)
            };

            var result = await _aiService.GetRecommendationsAsync(request);

            var response = result.Recommendations.Select(r => new RecommendedDestinationResponse
            {
                CityId = r.ItemId,
                CityName = r.Name,
                Country = r.Location,
                Description = r.Description,
                MatchScore = r.MatchScore
            }).ToList();

            return Ok(response);
        }

        #endregion


        private static DestinationResponseDTO MapToDto(Destination d)
        {
            return new DestinationResponseDTO
            {
                Id = d.Id,
                Name = d.Name,
                CityId = d.CityId,
                CityName = d.City?.Name ?? "Unknown",
                Description = d.Description,
                ImageUrl = d.ImageUrl,
                Rating = d.Rating
            };
        }

    }
}
