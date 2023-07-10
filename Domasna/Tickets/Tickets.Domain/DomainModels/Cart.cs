using Tickets.Domain.Identity;

namespace Tickets.Domain.DomainModels
{
    public class Cart : BaseEntity
    {
        public string UserID { get; set; }
        public AppUser User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
