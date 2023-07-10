using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Domain.DomainModels;

namespace Tickets.Services.Interface
{
    public interface IOrderService
    {
        List<Order> GetAllOrders(string userId);
		List<Order> GetAllOrders();
		Order GetOrderDetails(Guid? orderId);
    }
}
