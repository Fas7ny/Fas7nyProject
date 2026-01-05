using Fas7ny.Application.DTOs.City.Request;
using Fas7ny.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fas7ny.Application.DTOs.TouristPlace.Response
{
    public class TouristPlaceResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? OpeningHours { get; set; }
        public decimal EntryFee { get; set; }
        public int CityId { get; set; }

        public virtual CreateCityRequestDTO City { get; set; } = null!;
        public virtual ICollection<PackageDetailDto> PackageDetails { get; set; } = new List<PackageDetailDto>();
    }
}
