using Digiuth.DAL;
using Digiuth.Models;
using Digiuth.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager, AppDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<UserVM> usersVM = new List<UserVM>();
            List<AppUser> users;
            users = _userManager.Users.ToList();
            foreach (AppUser user in users)
            {
                UserVM userVM = new UserVM
                {
                    Id = user.Id,
                    FullName=user.FullName,
                    Image=user.Image,
                    Position=user.Position,
                    Phone=user.Phone,
                    IsVerified=user.IsVerified,
                    Website=user.Website,
                    Address=user.Address,
                    Facebook=user.Facebook,
                    Twitter=user.Twitter
                };
                usersVM.Add(userVM);
            }

            return View(usersVM);
        }

        public async Task<IActionResult> Active(string id, bool IsVerified)
        {
            if (id == null) return NotFound();
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            user.IsVerified = IsVerified;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Disable(string id, bool IsVerified)
        {
            if (id == null) return NotFound();
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            user.IsVerified = !IsVerified;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> DeleteUser(string id)
        {
            AppUser appUser = _db.Users.Include(x => x.Course).FirstOrDefault(s => s.Id == id);
            if (appUser == null) return RedirectToAction("Index", "Error404");
            foreach (var jobs in appUser.Course)
            {
                _db.Courses.Remove(jobs);
            }
            if (_db.Users.Count() > 1)
            {
                _db.Users.Remove(appUser);
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
