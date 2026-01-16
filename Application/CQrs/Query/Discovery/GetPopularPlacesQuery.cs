using Fas7ny.Application.CQrs.InterfaceCommandQuery;
using Fas7ny.Application.DTOs.TouristPlace.Response;

namespace Fas7ny.Application.CQrs.Query.Discovery
{
    public class GetPopularPlacesQuery : IQuery<Result<List<TouristPlaceResponseDTO>>>
    {
        public int Count { get; set; } = 20;
    }
}
