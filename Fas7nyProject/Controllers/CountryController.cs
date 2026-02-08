using Fas7ny.Application.Dtos.CountryDtos;
using Fas7ny.Domain.Entities.Fas7ny.Domain.Entities;
using Fas7ny.Domain.RepoInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fas7nyProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CountryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        #region CRUD

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> create([FromBody] CreateCountryDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var Country = new Country
            {
                Code = dto.Code,
                Name = dto.Name,
                IsActive = dto.IsActive
            };
            await _unitOfWork.Countries.AddAsync(Country);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(
              nameof(GetById),
              new { id = Country.Id },
              new
              {
                  Country.Name,

                  Country.IsActive,
                  Country.Code

              });
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest("id is not valid");
            var country = await _unitOfWork.Countries.GetByIdAsync(id);
            if (country == null) return BadRequest("country not found");
            return Ok(new
            {
                country.Name,
                country.IsActive,
                country.Code

            });

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var countries = await _unitOfWork.Countries.GetAllAsync();

            var response = countries.Select(a => new
            {
                a.Name,
                a.IsActive,
                a.Code
            });

            return Ok(response);
        }



        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCountryDto dto)
        {
            if (id <= 0) return BadRequest("id is not valid");
            var country = await _unitOfWork.Countries.GetByIdAsync(id);
            if (country == null) return BadRequest("country not found");
            country.Name = dto.Name;
            country.IsActive = dto.IsActive;
            country.Code = dto.Code;
            await _unitOfWork.Countries.UpdateAsync(country);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new
            {
                country.Name,
                country.IsActive,
                country.Code

            });
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("id is not valid ");
            var country = await _unitOfWork.Countries.GetByIdAsync(id);
            if (country == null) return BadRequest("country not found");
            await _unitOfWork.Countries.DeleteAsync(country);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new { message = "Activity deleted successfully" });

        }

        #endregion


        [HttpGet("country/{countryId:int}/city")]
        public async Task<IActionResult> GetCitiesByCountryId(int countryId)
        {
            if (countryId <= 0)
                return BadRequest("Invalid country ID");

            var country = await _unitOfWork.Countries.GetByIdAsync(countryId);
            if (country == null)
                return NotFound("Country not found");

            // Get all cities and filter by countryId
            var allCities = await _unitOfWork.Cities.GetAllAsync();
            var countryCities = allCities.Where(c => c.CountryId == countryId).ToList();

            return Ok(countryCities);
        }

    }
}
