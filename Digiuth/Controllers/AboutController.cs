using Digiuth.DAL;
using Digiuth.Models;
using Digiuth.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public AboutController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            AboutVM aboutVM = new AboutVM
            {
                AboutUs = _db.AboutUs.FirstOrDefault(),
                Testimonials = _db.Testimonials,
                WatchUs=_db.WatchUs.FirstOrDefault(),
                User=_db.Users,
                Blogs=_db.Blogs
            };
            return View(aboutVM);
        }
    }
}