namespace Fas7ny.Domain.Entities.Transpotrations
{
    public class Flight : Transportations
    {
        public string AirLine { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DepartureTime { get; set; }

        public DateTime ArivaTime { get; set; }
    }
}
