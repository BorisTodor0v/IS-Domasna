namespace Tickets.Domain.DomainModels
{
    public class OrderItem : BaseEntity
    {
        public Guid TicketID { get; set; }
        public Ticket OrderedTicket { get; set; }
        public Guid OrderID { get; set; }
        public Order Order { get; set; }
        public int quantity { get; set; }

    }
}
