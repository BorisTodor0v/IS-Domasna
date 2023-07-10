using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Domain.DomainModels;

namespace Tickets.Domain.DTO
{
	public class OrderDto
	{
		public LinkedList<OrderItem> OrderItems { get; set; }
		public int TotalPrice { get; set; }
	}
}
