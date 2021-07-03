using Digiuth.DAL;
using Digiuth.Models;
using Digiuth.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Bio = _db.Bios.FirstOrDefault(),
                AboutUs = _db.AboutUs.FirstOrDefault(),
                Testimonials = _db.Testimonials,
                WatchUs = _db.WatchUs.FirstOrDefault(),
                MainCategories = _db.MainCategories,
                OurEvents = _db.OurEvents,
                Blogs = _db.Blogs,
                Courses = _db.Courses.Where(x => x.IsFeatured && x.IsFeatured).ToList()
            };
            return View(homeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Subscribe([FromForm] string email)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            bool existSubscription = _db.Subscriptions.Any(e => e.Email == email);
            if (existSubscription)
            {
                return Ok(existSubscription);
            }
            Subscription subscription = new Subscription { Email = email };
            await _db.Subscriptions.AddAsync(subscription);
            await _db.SaveChangesAsync();
            return Ok(existSubscription);
        }
    }
}