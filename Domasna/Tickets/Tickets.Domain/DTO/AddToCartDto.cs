using Tickets.Domain.DomainModels;

namespace Tickets.Domain.DTO
{
    public class AddToCartDto
    {
        public Ticket SelectedTicket { get; set; }
        public Guid TicketID { get; set; }
        public int quantity { get; set; }
    }
}
