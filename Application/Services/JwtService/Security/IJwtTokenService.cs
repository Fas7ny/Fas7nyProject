using Fas7ny.Domain.Entities;

namespace Fas7ny.Application.Services.JwtService.Security
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(ApplicationUser user, IList<string> roles);

        string GenerateRefreshToken();

    }
}
