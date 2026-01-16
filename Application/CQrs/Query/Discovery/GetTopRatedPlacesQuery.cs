using Fas7ny.Application.CQrs.InterfaceCommandQuery;
using Fas7ny.Application.DTOs.TouristPlace.Response;

namespace Fas7ny.Application.CQrs.Query.Discovery
{
    public class GetTopRatedPlacesQuery : IQuery<Result<List<TouristPlaceResponseDTO>>>
    {
        public int Count { get; set; } = 10;
    }
}
