using Fas7ny.Application.CQrs.InterfaceCommandQuery;
using Fas7ny.Application.DTOs.Account.Response;

namespace Fas7ny.Application.CQrs.Query.Profile
{
    public class GetUserReviewsQuery : IQuery<Result<List<UserResponseDto>>>
    {
        public Guid UserId { get; set; }
    }
}
