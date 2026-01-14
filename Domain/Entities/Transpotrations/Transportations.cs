using Fas7ny.Domain.Enum;

namespace Fas7ny.Domain.Entities.Transpotrations
{
    public abstract class Transportations
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TransportType transportType { get; set; }
        public decimal Price { get; set; }
        public TimeSpan duration { get; set; }
    }
}
