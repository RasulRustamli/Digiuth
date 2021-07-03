using Digiuth.DAL;
using Digiuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Controllers
{
    public class TeamController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;
        public TeamController(UserManager<AppUser> userManager,
                                AppDbContext db,
                                SignInManager<AppUser> signInManager,
                                RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        } 
        public IActionResult Index()
        {
            return View(_db.Users.Where(x=>x.IsVerified && x.IsTeacher).ToList());
        }
        public IActionResult Detail(string id)
        {
            if (id == null) return NotFound();
            AppUser user = _db.Users.FirstOrDefault(x => x.Id == id && x.IsVerified);
            if (user==null) return NotFound();
            return View(user);
        }
    }
}
