namespace Fas7ny.Application.DTOs.Activity.Request
{
    public class CreateActivityRequestDTO
    {
        public string ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string Description { get; set; }
        public string ActivityType { get; set; } = string.Empty;


    }
}
