using Fas7ny.Application.DTOs.City.Request;
using Fas7ny.Application.DTOs.City.Response;
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
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public CityController(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            [FromForm] CreateCityRequestDTO dto,
             IFormFile? image)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string? imagePath = null;

            if (image != null)
                imagePath = await _fileService.SaveFileAsync(image, "City");

            var city = new City
            {
                Name = dto.Name,
                Description = dto.Description,
                CountryId = dto.CountryId,
                ImageUrl = imagePath
            };

            await _unitOfWork.Cities.AddAsync(city);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = city.Id },
                MapToDto(city)
            );
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid city id");

            var city = await _unitOfWork.Cities.GetByIdAsync(id);
            if (city == null)
                return NotFound($"City with id {id} not found");

            return Ok(new
            {
                City = MapToDto(city),
                PictureUrl = ImageUrlHelper.BuildImageUrl(
                   "http://Fas7ny.runasp.net",
                   "city",
                   city.ImageUrl)
            });

        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCityRequestDTO dto)
        {
            if (id <= 0)
                return BadRequest("Invalid city id");

            var city = await _unitOfWork.Cities.GetByIdAsync(id);
            if (city == null)
                return NotFound();

            city.Name = dto.Name;
            city.Description = dto.Description;
            city.ImageUrl = dto.ImageUrl;
            city.CountryId = dto.CountryId;

            await _unitOfWork.Cities.UpdateAsync(city);
            await _unitOfWork.SaveChangesAsync();

            return Ok(MapToDto(city));
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid city id");

            var city = await _unitOfWork.Cities.GetByIdAsync(id);
            if (city == null)
                return NotFound();

            await _unitOfWork.Cities.DeleteAsync(city);
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

                var (cities, totalCount) =
                    await _unitOfWork.Cities.GetPagedAsync(page.Value, pageSize.Value);

                return Ok(new
                {
                    page,
                    pageSize,
                    totalCount,
                    totalPages = (int)Math.Ceiling(totalCount / (double)pageSize.Value),
                    items = cities.Select(MapToDto)
                });
            }

            var allCities = await _unitOfWork.Cities.GetAllAsync();
            return Ok(allCities.Select(MapToDto));

        }



        // MAPPER
        private static CityResponseDTO MapToDto(City city) => new()
        {
            Id = city.Id,
            Name = city.Name,
            Description = city.Description,
            CountryId = city.CountryId,
            ImageUrl = city.ImageUrl
        };
    }
}
