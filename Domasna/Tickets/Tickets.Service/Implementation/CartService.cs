using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain.DomainModels;
using Tickets.Domain.DTO;
using Tickets.Repository.Interface;
using Tickets.Services.Interface;

namespace Tickets.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IUserRepository _userRepository;
        public CartService(IRepository<Cart> cartRepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<OrderItem> orderItemRepository)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }
        public CartDto CartDetails(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

            var userCart = loggedInUser.UserCart;

            var allTickets = userCart.CartItems.ToList();
            
            var ticketsLinkedList = new LinkedList<CartItem>(allTickets);
            
            var CartItemsPrice = allTickets.Select(x => new
            {
                Price = x.Ticket.Price,
                Quantity = x.quantity
            }).ToList();

            var totalPrice = 0;
            foreach (var item in CartItemsPrice)
            {
                totalPrice += item.Quantity * item.Price;
            }

            CartDto cartDto = new CartDto
            {
                Tickets = ticketsLinkedList,
                TotalPrice = totalPrice
            };
            return cartDto;
        }

        public bool DeleteCartItem(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCart = loggedInUser.UserCart;
                var itemtoDelete = userCart.CartItems.Where(x => x.TicketID.Equals(id)).FirstOrDefault();
                userCart.CartItems.Remove(itemtoDelete);
                this._cartRepository.Update(userCart);
                return true;
            }
            return false;
        }

        public bool OrderCart(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userCart = loggedInUser.UserCart;

                Order order = new Order
                {
                    ID = Guid.NewGuid(),
                    User = loggedInUser,
                    UserID = userId
                };

                this._orderRepository.Insert(order);

                List<OrderItem> OrderItems = new List<OrderItem>();
                var result = userCart.CartItems.Select(x => new OrderItem
                {
                    TicketID = x.TicketID,
                    OrderedTicket = x.Ticket,
                    OrderID = order.ID,
                    Order = order,
                    quantity = x.quantity
                }).ToList();
                OrderItems.AddRange(result);

                foreach (var Item in OrderItems)
                {
                    this._orderItemRepository.Insert(Item);
                }
                
                loggedInUser.UserCart.CartItems.Clear();
                
                this._userRepository.Update(loggedInUser);
                return true;
            }
            return false;
        }
    }
}
