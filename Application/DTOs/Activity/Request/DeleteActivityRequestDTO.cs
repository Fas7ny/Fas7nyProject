using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Activity.Request
{
    public class DeleteActivityRequestDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
