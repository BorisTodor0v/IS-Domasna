using System;
using System.Reflection.Metadata;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Tickets.Domain.DomainModels;
using Tickets.Services.Implementation;
using Tickets.Services.Interface;

namespace Tickets.Web.Controllers
{

    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ITicketService _ticketService;

        public CategoriesController(ICategoryService categoryService, ITicketService ticketService)
        {
            _categoryService = categoryService;
            _ticketService = ticketService;
        }
        // GET: Categories
        public IActionResult Index()
        {
            var allTickets = _categoryService.GetAllCategories();
            return View(allTickets);
        }
        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.CreateNewCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult TicketsFromCategory(Guid? categoryId)
        {
            var categoryTickets = _ticketService.GetTicketByCategory(categoryId);
            var category = _categoryService.GetCategory((Guid)categoryId);
            ViewBag.CategoryId = category.ID;
            ViewBag.CategoryName = category.Name;
			return View(categoryTickets);
        }

        public IActionResult ExportTicketsToExcel(Guid? categoryId)
        {
            var categoryTickets = _ticketService.GetTicketByCategory(categoryId);
			var category = _categoryService.GetCategory((Guid)categoryId);

			var excelPackage = new ExcelPackage();
            var worksheet = excelPackage.Workbook.Worksheets.Add("Tickets");

            worksheet.Cells[1, 1].Value = "Title";
            worksheet.Cells[1, 2].Value = "Description";
            worksheet.Cells[1, 3].Value = "Price";
            worksheet.Cells[1, 4].Value = "Date";

            var row = 2;
            foreach (var ticket in categoryTickets)
            {
                worksheet.Cells[row, 1].Value = ticket.Title;
                worksheet.Cells[row, 2].Value = ticket.Description;
                worksheet.Cells[row, 3].Value = ticket.Price;
                worksheet.Cells[row, 4].Value = ticket.date.ToShortDateString();

                row++;
            }

            worksheet.Cells.AutoFitColumns();

            var fileContents = excelPackage.GetAsByteArray();

            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = $"{category.Name}.xlsx";

            return File(fileContents, contentType, fileName);
        }

    }
}
