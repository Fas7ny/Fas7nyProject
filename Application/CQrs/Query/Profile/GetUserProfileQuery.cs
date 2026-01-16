using Fas7ny.Application.CQrs.InterfaceCommandQuery;
using Fas7ny.Application.DTOs.Account.Response;

namespace Fas7ny.Application.CQrs.Query.Profile
{
    public class GetUserProfileQuery : IQuery<Result<UserResponseDto>>
    {
        public Guid UserId { get; set; }
    }
}
