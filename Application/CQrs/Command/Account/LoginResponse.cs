namespace Fas7ny.Application.CQrs.Command.Account
{
    public class LoginResponse
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
