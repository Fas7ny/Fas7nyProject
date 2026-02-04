using Fas7ny.Application.DTOs.UserPerfernce.Request;
using Fas7ny.Domain.Entities;
using Fas7ny.Domain.RepoInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fas7nyProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPreferencesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserPreferencesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("SavePreferences")]
        public async Task<IActionResult> SavePreferences([FromBody] SavePreferencesDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { message = "Invalid preference data." });
            }
            var preference = new UserPreference
            {
                Budget = dto.Budget,
                //   CreatedAt = dto.CreatedAt,
                StayDuration = dto.StayDuration,
                UserId = dto.UserId,
                Username = dto.Username,
                CategoryPreference = dto.CategoryPreference
            };
            int[] allowedDurations = Enumerable.Range(1, 10).ToArray();
            decimal[] allowedBudgets = Enumerable.Range(1, 20).Select(i => i * 100m).ToArray();

            var errors = new List<string>();

            if (!allowedDurations.Contains(preference.StayDuration))
            {
                errors.Add("Invalid stay duration.");
            }

            if (!allowedBudgets.Contains(preference.Budget))
            {
                errors.Add("Invalid budget.");
            }

            if (!Enum.IsDefined(typeof(CategoryPreference), preference.CategoryPreference))
            {
                errors.Add("Invalid category preference.");
            }

            if (errors.Any())
            {
                return BadRequest(new { message = "Validation errors", errors });
            }


            var existingPreference = await _unitOfWork.UserPreferences
                .FindAsync(p => p.Username.ToLower() == dto.Username.ToLower());

            if (existingPreference != null)
            {
                existingPreference.StayDuration = dto.StayDuration;
                existingPreference.Budget = dto.Budget;
                existingPreference.CategoryPreference = dto.CategoryPreference;
            }
            else
            {
                await _unitOfWork.UserPreferences.AddAsync(preference);
            }

            await _unitOfWork.SaveChangesAsync();
            return Ok(new { message = "Preferences saved successfully." });
        }

    }
}
