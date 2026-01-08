using Fas7ny.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fas7ny.Application.DTOs.City.Response
{
    public class CityResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
