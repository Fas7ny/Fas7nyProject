using Fas7ny.Application.CQrs.InterfaceCommandQuery;
using Fas7ny.Application.DTOs.TouristPlace.Response;

namespace Fas7ny.Application.CQrs.Query.Search
{
    public class GetPlaceDetailsQuery : IQuery<Result<TouristPlaceResponseDTO>>
    {
        public Guid PlaceId { get; set; }
    }
}
