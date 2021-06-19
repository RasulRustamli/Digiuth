using Digiuth.DAL;
using Digiuth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Digiuth.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _db;
        public EventController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.OurEvents.ToList());
        }
        public IActionResult Detail(int? id)
        {
            ViewBag.MainCategories = _db.MainCategories.ToList();
            if (id == null) return NotFound();
            OurEvent ourEvent = _db.OurEvents.Include(x => x.MainCategory).FirstOrDefault(x => x.Id == id);
            if (ourEvent == null) return NotFound();
            return View(ourEvent);
        }
    }
}
