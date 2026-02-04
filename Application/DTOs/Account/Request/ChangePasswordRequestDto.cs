using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Account.Request
{
    public class ChangePasswordRequestDto
    {
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        [Compare(nameof(ConfirmNewPassword), ErrorMessage = "Passwords do not match")]

        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
