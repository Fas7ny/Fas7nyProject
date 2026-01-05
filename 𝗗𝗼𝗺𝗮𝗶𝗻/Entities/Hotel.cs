using Fas7ny.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fas7ny.Domain.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal PricePerNight { get; set; }
        public string? ImageUrl { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual ICollection<HotelRoom> HotelRooms { get; set; } = new List<HotelRoom>();
        public virtual ICollection<Package> Packages { get; set; } = new List<Package>();
    }
}
