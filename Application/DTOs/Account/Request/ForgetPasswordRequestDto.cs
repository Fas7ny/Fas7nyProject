using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Account.Request
{
    public class ForgetPasswordRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
