using CoolEvents.Data;
using CoolEvents.Models;
using CoolEvents.VIewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoolEvents.Controllers
{
    [Authorize]
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            return _context.ApplicationUsers != null ?
                          View(await _context.ApplicationUsers.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AppUsers'  is null.");
        }

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        //public IActionResult Create()
        //{
        //    return View();
        //}
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(ApplicationUserCreateViewModel applicationUserCreateViewModel)
        {
            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserName = applicationUserCreateViewModel.Username,
                FirstName = applicationUserCreateViewModel.FirstName,
                LastName = applicationUserCreateViewModel.LastName,
            };

            //May be not valid
            if (!ModelState.IsValid)
            {
                _context.ApplicationUsers.Add(applicationUser);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var user = await _context.ApplicationUsers.FindAsync(id);
            ApplicationUserEditView uvm = new ApplicationUserEditView()
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.PasswordHash
            };
            if (user == null)
            {
                return NotFound();
            }
            return View(uvm);
        }

        //TO FIX

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ApplicationUserEditView user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser edited = new ApplicationUser()
                    {
                        Id = user.Id,
                        UserName = user.Username,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PasswordHash = user.Password
                    };
                    _context.ApplicationUsers.Update(edited);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        private bool UserExists(string id)
        {
            return (_context.ApplicationUsers?.Any(u => u.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Events'  is null.");
            }
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
