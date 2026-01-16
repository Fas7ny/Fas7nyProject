using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.SearchLog.Request
{
    public class SearchLogRequestDTO
    {
        [Required(ErrorMessage = "User ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid User ID is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Search term is required")]
        [StringLength(200, ErrorMessage = "Search term cannot exceed 200 characters")]
        [MinLength(1, ErrorMessage = "Search term cannot be empty")]
        public string SearchTerm { get; set; }

        [Required(ErrorMessage = "Search category is required")]
        [StringLength(50, ErrorMessage = "Search category cannot exceed 50 characters")]
        public string SearchCategory { get; set; }

        [Required(ErrorMessage = "Search date is required")]
        public DateTime SearchDate { get; set; }
    }
}
