using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Review.Request
{
    public class DeleteReviewRequest
    {
        [Required(ErrorMessage = "Review ID is required")]
        public int ReviewId { get; set; }


    }
}
