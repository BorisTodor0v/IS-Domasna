using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain.DomainModels;
using Tickets.Domain.Identity;
using Tickets.Services.Implementation;
using Tickets.Services.Interface;

namespace Tickets.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        // GET: Users
        public IActionResult Index()
        {
            var users = userService.GetAll();
            return View(users);
        }

        private bool UserExists(string userId)
        {
            return this.userService.GetUserDetails(userId) != null;
        }

        // GET: User/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = userService.GetUserDetails(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Id, FirstName, LastName, Role")] AppUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            try
            {
                var existingUser = userService.GetUserDetails(id);

                if (existingUser == null)
                {
                    return NotFound();
                }

                existingUser.Role = user.Role;

                userService.UpdateUser(existingUser);
            }
            catch (Exception)
            {
                if (!UserExists(user.Id))
                {
                    return NotFound();
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred while updating the user.");
                    return View(user);
                }
            }
            return RedirectToAction(nameof(Index));
            //}
            //return View(user);
        }
    }
}
