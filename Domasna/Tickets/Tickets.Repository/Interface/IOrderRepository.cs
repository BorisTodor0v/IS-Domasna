using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Domain.DomainModels;

namespace Tickets.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders(string userId);
		List<Order> GetAllOrders();
		Order Get(Guid? orderId);
    }
}
