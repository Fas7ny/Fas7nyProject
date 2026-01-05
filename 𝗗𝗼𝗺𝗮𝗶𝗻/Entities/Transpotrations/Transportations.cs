using Fas7ny.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
