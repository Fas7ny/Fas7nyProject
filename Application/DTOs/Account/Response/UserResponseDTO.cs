namespace Fas7ny.Application.DTOs.Account.Response
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PreferencesJson { get; set; }
    }
}
