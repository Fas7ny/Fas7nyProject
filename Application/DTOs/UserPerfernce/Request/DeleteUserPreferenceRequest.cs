using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.UserPerfernce.Request
{
    public class DeleteUserPreferenceRequest
    {
        [Required(ErrorMessage = "Preference ID is required")]
        public int PreferenceId { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }
    }
}
