namespace Fas7ny.Application.DTOs.Transportations.BusDtos.Response
{
    public class BusDtoResponseDTO
    {
        public string Company { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DepartureTime { get; set; }

        public DateTime ArivaTime { get; set; }
    }
}
