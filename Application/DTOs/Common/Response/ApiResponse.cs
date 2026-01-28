namespace Fas7ny.Application.DTOs.Common.Response
{
    public class ApiResponse
    {
        public virtual bool Success { get; set; }
        public virtual string Message { get; set; }

        public virtual List<string> Errors { get; set; }
        public virtual DateTime Timestamp { get; set; }
    }
}
