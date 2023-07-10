using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Domain.DomainModels;
using Tickets.Repository.Interface;
using Tickets.Services.Interface;

namespace Tickets.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IUserRepository userRepository;

        public OrderService(IOrderRepository order, IUserRepository user) 
        {
            orderRepository = order;
            userRepository = user;
        }
        public List<Order> GetAllOrders(string userId)
        {
            return orderRepository.GetAllOrders(userId);
        }

		public List<Order> GetAllOrders()
		{
			return orderRepository.GetAllOrders();
		}

		public Order GetOrderDetails(Guid? orderId)
        {
            var order = orderRepository.Get(orderId);
            if (order == null)
            {
                return null;
            }
            return order;
        }
    }
}
