using Tickets.Domain.Identity;

namespace Tickets.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserID { get; set; }
        public AppUser User { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
