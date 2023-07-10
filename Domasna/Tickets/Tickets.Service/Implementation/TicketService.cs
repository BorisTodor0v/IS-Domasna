using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain.DomainModels;
using Tickets.Domain.DTO;
using Tickets.Domain.Identity;
using Tickets.Repository.Interface;
using Tickets.Services.Interface;

namespace Tickets.Services.Implementation
{

    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly ITicketRepository _ticketHelpRepository;
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IUserRepository _userRepository;

        public TicketService(IRepository<Ticket> ticketRepository, IUserRepository userRepository, IRepository<CartItem> cartItemRepository, ITicketRepository ticketHelpRepository) 
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _cartItemRepository = cartItemRepository;
            _ticketHelpRepository = ticketHelpRepository;
        }
        public bool AddToCart(AddToCartDto CartItem, string userID)
        {

            var user = this._userRepository.Get(userID);

            var userCart = user.UserCart;

            if (CartItem.TicketID != null && userCart != null)
            {
                var ticket = this.GetTicketDetails(CartItem.TicketID);
                if (ticket != null)
                {
                    CartItem cartItem = new CartItem
                    {
                        Ticket = ticket,
                        TicketID = ticket.ID,
                        Cart = userCart,
                        CartID = userCart.ID,
                        quantity = CartItem.quantity
                    };
                    this._cartItemRepository.Insert(cartItem);
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewTicket(Ticket ticket)
        {
            this._ticketRepository.Insert(ticket);
        }

        public void DeleteTicket(Guid Id)
        {
            var ticket = this.GetTicketDetails(Id);
            this._ticketRepository.Delete(ticket);
        }

        public List<Ticket> GetAllTickets()
        {
            return this._ticketHelpRepository.GetAll();
        }

        public List<Ticket> GetByDate(DateTime? date)
        {
            return this._ticketHelpRepository.GetByDate(date);
        }

        public AddToCartDto GetCartInfo(Guid? Id)
        {
            var ticket = this.GetTicketDetails(Id);
            AddToCartDto model = new AddToCartDto
            {
                SelectedTicket = ticket,
                TicketID = ticket.ID,
                quantity = 1
            };
            return model;
        }

        public List<Ticket> GetTicketByCategory(Guid? categoryId)
        {
            return this._ticketHelpRepository.GetByCategory(categoryId);
        }

        public Ticket GetTicketDetails(Guid? Id)
        {
            return this._ticketHelpRepository.Get(Id);
        }

        public void UpdateTicket(Ticket ticket)
        {
            this._ticketRepository.Update(ticket);
        }
    }
}
