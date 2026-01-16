using Fas7ny.Application.CQrs.InterfaceCommandQuery;

namespace Fas7ny.Application.CQrs.Command.Account
{
    public class LoginUserCommand : ICommand<Result<LoginResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
