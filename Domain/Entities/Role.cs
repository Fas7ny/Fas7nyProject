using Microsoft.AspNetCore.Identity;

namespace Fas7ny.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public int Id { get; set; }
        public string role { get; set; } = string.Empty;
        public string name { get; set; }

    }
}
