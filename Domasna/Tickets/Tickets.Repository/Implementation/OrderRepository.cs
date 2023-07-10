using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain.DomainModels;
using Tickets.Repository.Interface;

namespace Tickets.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> orders;
        string errorMessage = string.Empty;
        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.orders = context.Set<Order>();
        }

        public Order Get(Guid? orderId)
        {
			return orders
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.OrderedTicket)
				.SingleOrDefault(o => o.ID == orderId);
		}

		public List<Order> GetAllOrders(string userId)
        {
            return orders
                .Include(x => x.OrderItems)
                .Include(x => x.User)
                .Include("OrderItems.OrderedTicket")
                .Where(x => x.UserID == userId)
                .ToListAsync().Result;
        }

		public List<Order> GetAllOrders()
		{
			return orders
				.Include(x => x.OrderItems)
				.Include(x => x.User)
				.Include("OrderItems.OrderedTicket")
				.ToListAsync().Result;
		}
	}
}
