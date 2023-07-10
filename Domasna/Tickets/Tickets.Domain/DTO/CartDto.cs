using Tickets.Domain.DomainModels;

namespace Tickets.Domain.DTO
{
    public class CartDto
    {
        public LinkedList<CartItem> Tickets { get; set; }
        public int TotalPrice { get; set; }
    }
}
