using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Core;
using Tickets.Domain.DomainModels;
using Tickets.Domain.DTO;
using Tickets.Repository;
using Tickets.Services.Interface;
using Tickets.Web.Data;

namespace Tickets.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService) 
        {
            _cartService = cartService;
        }

        //Show all tickets in cart
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(this._cartService.CartDetails(userId) != null)
            {
                return View(this._cartService.CartDetails(userId));
            }
            return View();
        }

        //Delete one ticket from cart
        public IActionResult Delete(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(this._cartService.DeleteCartItem(userId, id))
            {
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
            
        }

        //Add tickets from cart to Order
        public IActionResult Order()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (this._cartService.OrderCart(userId))
            {
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
        }
    }
}
