using Fas7nyProject.Presentation.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.UserPerfernce.Request
{
    public class SavePreferencesDto
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public int StayDuration { get; set; }
        [Range(100, 1000000, ErrorMessage = "Budget must be between 100 and 1,000,000")]

        public decimal Budget { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public CategoryPreference CategoryPreference { get; set; }
    }
}
