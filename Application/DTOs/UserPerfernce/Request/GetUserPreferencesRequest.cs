using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.UserPerfernce.Request
{
    public class GetUserPreferencesRequest
    {
        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }
    }
}
