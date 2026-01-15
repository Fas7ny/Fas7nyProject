namespace Fas7ny.Application.DTOs.Transportations.BusDtos.Response
{
    public class BusDtoResponseDTO
    {
        public int Id { get; set; }
        public string BusNumber { get; set; }
        public string OperatorName { get; set; }
        public int Capacity { get; set; }
        public string BusType { get; set; }
        public bool IsActive { get; set; }
    }
}
