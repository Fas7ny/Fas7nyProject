using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.SearchLog.Request
{
    public class SearchQueryRequestDTO
    {
        [Required(ErrorMessage = "Query is required")]
        [StringLength(200, ErrorMessage = "Query cannot exceed 200 characters")]
        [MinLength(1, ErrorMessage = "Query cannot be empty")]
        public string Query { get; set; }

        [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
        public string Category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Valid City ID is required")]
        public int? CityId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Minimum price must be greater than or equal to 0")]
        public decimal? MinPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Maximum price must be greater than or equal to 0")]
        public decimal? MaxPrice { get; set; }
    }
}
