using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain.DomainModels;
using Tickets.Domain.DTO;
using Tickets.Services.Interface;

namespace Tickets.Web.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ICategoryService _categoryService;

        public TicketsController(ITicketService ticketService, ICategoryService categoryService)
        {
            _ticketService = ticketService;
            _categoryService = categoryService;
        }

        // GET: Tickets
        public IActionResult Index(DateTime? selectedDate)
        {
            var allTickets = _ticketService.GetAllTickets();
            var categories = _categoryService.GetAllCategories();

            if (selectedDate.HasValue)
            {
                allTickets = _ticketService.GetByDate(selectedDate);
            }

            ViewBag.SelectedDate = selectedDate;
            ViewBag.Categories = categories != null ? new SelectList(categories, "ID", "Name") : null;

            return View(allTickets);
        }

        // GET: Tickets/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = this._ticketService.GetTicketDetails(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            var categories = _categoryService.GetAllCategories();
            ViewBag.Categories = categories != null ? new SelectList(categories, "ID", "Name") : null;

            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Title,Description,Price,date,CategoryId")] Ticket ticket)
        {
            //if (ModelState.IsValid)
            //{
                this._ticketService.CreateNewTicket(ticket);
            var categories = _categoryService.GetAllCategories();
            ViewBag.Categories = categories != null ? new SelectList(categories, "ID", "Name") : null;
            return RedirectToAction(nameof(Index));
            //}
            
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = this._ticketService.GetTicketDetails(id);
            if (ticket == null)
            {
                return NotFound();
            }
            var categories = _categoryService.GetAllCategories();
            ViewBag.Categories = categories != null ? new SelectList(categories, "ID", "Name") : null;

            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("ID,Title,Description,Price,date,CategoryId")] Ticket ticket)
        {
            if (id != ticket.ID)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    var categories = _categoryService.GetAllCategories();
                    ViewBag.Categories = categories != null ? new SelectList(categories, "ID", "Name") : null;
                    this._ticketService.UpdateTicket(ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = this._ticketService.GetTicketDetails(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this._ticketService.DeleteTicket(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(Guid id)
        {
            return this._ticketService.GetTicketDetails(id) != null;
        }

        // Add To Cart - cekor 1: proverka na proizvod i menuvanje kvantitet
        public IActionResult AddToCart(Guid? id)
        {
            var model = this._ticketService.GetCartInfo(id);
            return View(model);
        }

        // POST: Add to cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart([Bind("TicketID","quantity")] AddToCartDto item)
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._ticketService.AddToCart(item, userID);

            if(result)
            {
                return RedirectToAction("Index", "Tickets");
            }

            return View(item);
        }
    }

}
