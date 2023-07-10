namespace Tickets.Domain.DomainModels
{
    public class CartItem : BaseEntity
    {
        public Guid TicketID { get; set; }
        public Ticket Ticket { get; set; }
        public Guid CartID { get; set; }
        public Cart Cart { get; set; }
        public int quantity { get; set; }
    }
}
