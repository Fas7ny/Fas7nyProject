using Fas7ny.Application.CQrs.InterfaceCommandQuery;
using Fas7ny.Application.DTOs.City.Response;

namespace Fas7ny.Application.CQrs.Query.Discovery
{
    public class GetFeaturedCitiesQuery : IQuery<Result<List<CityResponseDTO>>>
    {
        public int Count { get; set; } = 10;
    }
}
