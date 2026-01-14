namespace Fas7ny.Domain.Entities.Transpotrations
{
    public class Bus : Transportations
    {
        public string Company { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DepartureTime { get; set; }

        public DateTime ArivaTime { get; set; }
    }
}
