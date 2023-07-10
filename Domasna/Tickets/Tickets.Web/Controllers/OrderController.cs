using System.Security.Claims;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using Tickets.Domain.DomainModels;
using Tickets.Domain.DTO;
using Tickets.Services.Interface;

namespace Tickets.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        //GET: Order
        public IActionResult Index()
        {
			string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var orders = orderService.GetAllOrders(userID);
            return View(orders);
        }
        //GET: Order/Details/5
        public IActionResult Details(Guid? orderId)
        {
            var order = orderService.GetOrderDetails(orderId);
            if( order != null)
            {
				var orderItems = order.OrderItems.ToList();
				var orderItemsLinkedList = new LinkedList<OrderItem>(orderItems);
				var totalPrice = 0;
				foreach (var item in orderItems)
				{
					totalPrice += item.OrderedTicket.Price * item.quantity;
				}
				OrderDto orderDto = new OrderDto
				{
					OrderItems = orderItemsLinkedList,
					TotalPrice = totalPrice,
				};

				return View(orderDto);
			}
            return RedirectToAction("Index");
        }
        public IActionResult ExportOrders()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = orderService.GetAllOrders(userId);

            Document document = new Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

            document.Open();
            document.Add(new Paragraph("********************************************** Your Orders **********************************************"));
            foreach (var order in orders)
            {
                var totalPrice = 0;

                document.Add(new Paragraph($"Order ID: {order.ID}"));
                
                foreach (var orderItem in order.OrderItems)
                {
                    document.Add(new Paragraph($"Item: {orderItem.OrderedTicket.Title}"));
                    document.Add(new Paragraph($"Description: {orderItem.OrderedTicket.Description}"));
                    document.Add(new Paragraph($"Date: {orderItem.OrderedTicket.date.Date.ToShortDateString()}"));
                    document.Add(new Paragraph($"Quantity: {orderItem.quantity}"));
                    document.Add(new Paragraph($"Individual Price (мкд): {orderItem.OrderedTicket.Price}"));
                    document.Add(new Paragraph($"Subtotal(мкд): {orderItem.OrderedTicket.Price * orderItem.quantity}"));
                    document.Add(new Paragraph("--------------------------------------------------------"));
                    totalPrice += (orderItem.OrderedTicket.Price * orderItem.quantity);
                }
                document.Add(new Paragraph($"Total(мкд): {totalPrice}"));
                document.Add(new Paragraph("=========================================================================="));
            }

            document.Close();

            return File(memoryStream.ToArray(), "application/pdf", "orders.pdf");
        }
    }
}
