using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Domain.DomainModels;
using Tickets.Domain.DTO;

namespace Tickets.Services.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();
        List<Ticket> GetTicketByCategory(Guid? categoryId);
        List<Ticket> GetByDate(DateTime? date);
        Ticket GetTicketDetails(Guid? Id);
        void CreateNewTicket(Ticket ticket);
        void UpdateTicket(Ticket ticket);
        void DeleteTicket(Guid Id);
        AddToCartDto GetCartInfo(Guid? Id);
        bool AddToCart(AddToCartDto CartItem, string userID);
    }
}
