using Microsoft.AspNetCore.Identity;
using Tickets.Domain.DomainModels;

namespace Tickets.Domain.Identity
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Role { get; set; }
        public virtual Cart UserCart { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
