namespace Fas7ny.Domain.Entities
{
    public class SearchLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Query { get; set; } = string.Empty;
        public DateTime SearchDate { get; set; } = DateTime.UtcNow;

        public virtual User User { get; set; } = null!;
    }
}
