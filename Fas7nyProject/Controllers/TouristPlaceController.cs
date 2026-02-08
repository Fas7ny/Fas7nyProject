using Fas7ny.Application.DTOs.TouristPlace.Request;
using Fas7ny.Application.DTOs.TouristPlace.Response;
using Fas7ny.Application.Options;
using Fas7ny.Application.ServiceInterfaces;
using Fas7ny.Domain.Entities;
using Fas7ny.Domain.RepoInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fas7nyProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TouristPlaceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public TouristPlaceController(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;

        }


        #region CRUD

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            [FromForm] CreateTouristPlaceRequestDTO dto,
             IFormFile? image)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string? imagePath = null;

            if (image != null)
                imagePath = await _fileService.SaveFileAsync(image, "City");

            var place = new TouristPlace
            {
                Name = dto.Name,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                CityId = dto.CityId,
                EntryFee = dto.EntryFee,
                ImageUrl = imagePath,
            };

            await _unitOfWork.TouristPlaces.AddAsync(place);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = place.Id },
                MapToDto(place)
            );
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid city id");

            var place = await _unitOfWork.TouristPlaces.GetByIdAsync(id);
            if (place == null)
                return NotFound($"place with id {id} not found");

            return Ok(MapToDto(place));
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTouristPlaceRequestDTO dto)
        {
            if (id <= 0)
                return BadRequest("Invalid place id");

            var place = await _unitOfWork.TouristPlaces.GetByIdAsync(id);
            if (place == null)
                return NotFound();

            place.Id = id;
            place.Name = dto.Name;
            place.Description = dto.Description;
            place.EntryFee = dto.EntryFee;


            await _unitOfWork.TouristPlaces.UpdateAsync(place);
            await _unitOfWork.SaveChangesAsync();

            return Ok(MapToDto(place));
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid place id");

            var place = await _unitOfWork.TouristPlaces.GetByIdAsync(id);
            if (place == null)
                return NotFound();

            await _unitOfWork.TouristPlaces.DeleteAsync(place);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int? page,
            [FromQuery] int? pageSize)
        {
            if (page.HasValue && pageSize.HasValue)
            {
                if (page <= 0 || pageSize <= 0)
                    return BadRequest("Invalid pagination values");

                var (places, totalCount) =
                    await _unitOfWork.TouristPlaces.GetPagedAsync(page.Value, pageSize.Value);

                return Ok(new
                {
                    page,
                    pageSize,
                    totalCount,
                    totalPages = (int)Math.Ceiling(totalCount / (double)pageSize.Value),
                    items = places.Select(MapToDto)
                });
            }

            var allPlaces = await _unitOfWork.TouristPlaces.GetAllAsync();
            return Ok(allPlaces.Select(MapToDto));
        }

        [HttpGet("cities/{cityId:int}/tourist-Placses")]
        public async Task<IActionResult> GetActivitiesByCityId(int cityId)
        {
            if (cityId <= 0)
                return BadRequest("Invalid city ID");

            var city = await _unitOfWork.Cities.GetByIdAsync(cityId);
            if (city == null)
                return NotFound("City not found");

            // Get all activities and filter by cityId
            var allPlacses = await _unitOfWork.TouristPlaces.GetAllAsync();
            var cityTouristPlace = allPlacses.Where(a => a.CityId == cityId).ToList();

            return Ok(cityTouristPlace);
        }


        #endregion
        // MAPPER
        private static TouristPlaceResponseDTO MapToDto(TouristPlace place) => new()
        {
            CityId = place.CityId,
            Name = place.Name,
            Description = place.Description,
            EntryFee = place.EntryFee,
            ImageUrl = ImageUrlHelper.BuildImageUrl(
            "http://Fas7ny.runasp.net",
            "TouristPlace",
             place.ImageUrl)

        };
    }
}
