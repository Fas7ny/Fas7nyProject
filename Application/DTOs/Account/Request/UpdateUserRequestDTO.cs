using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Account.Request
{
    public class UpdateUserRequestDTO
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Full name must be between 3 and 100 characters")]
        public string FullName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        public string PreferencesJson { get; set; }
    }
}
