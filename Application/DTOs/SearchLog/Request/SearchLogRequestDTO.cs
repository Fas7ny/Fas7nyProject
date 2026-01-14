using Fas7ny.Application.DTOs.Account.Response;

namespace Fas7ny.Application.DTOs.SearchLog.Request
{
    public class SearchLogRequestDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Query { get; set; } = string.Empty;
        public DateTime SearchDate { get; set; } = DateTime.UtcNow;

        public virtual UserResponseDto User { get; set; } = null!;
    }
}
