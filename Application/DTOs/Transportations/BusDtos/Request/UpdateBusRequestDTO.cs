using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Transportations.BusDtos.Request
{
    public class UpdateBusRequestDTO
    {
        [Required(ErrorMessage = "Bus ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid Bus ID is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Bus number is required")]
        [StringLength(20, ErrorMessage = "Bus number cannot exceed 20 characters")]
        public string BusNumber { get; set; }

        [Required(ErrorMessage = "Operator name is required")]
        [StringLength(100, ErrorMessage = "Operator name cannot exceed 100 characters")]
        public string OperatorName { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, 100, ErrorMessage = "Capacity must be between 1 and 100")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Bus type is required")]
        [StringLength(50, ErrorMessage = "Bus type cannot exceed 50 characters")]
        public string BusType { get; set; }
    }
}
