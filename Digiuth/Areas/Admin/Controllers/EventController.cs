using Digiuth.DAL;
using Digiuth.Extentions;
using Digiuth.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public EventController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_db.OurEvents.Include(x => x.MainCategory).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.MainCategory = _db.MainCategories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OurEvent ourEvent)
        {
            ViewBag.MainCategory = _db.MainCategories.ToList();
            if (!ModelState.IsValid) return View();
            #region Photo
            if (!ModelState.IsValid) return View();
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!ourEvent.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                return View();
            }
            if (ourEvent.Photo.MaxLength(1000))
            {
                ModelState.AddModelError("Photo", "Shekilin olchusu max 1000kb ola biler");
                return View();
            }
            string path = Path.Combine("assets", "img", "event");
            string filename = await ourEvent.Photo.SaveImg(_env.WebRootPath, path);
            #endregion
            OurEvent newEvent = new OurEvent();
            newEvent.Image = filename;
            newEvent.Title = ourEvent.Title;
            newEvent.Email = ourEvent.Email;
            newEvent.ShortDesc = ourEvent.ShortDesc;
            newEvent.LongDesc = ourEvent.LongDesc;
            newEvent.StartTime = ourEvent.StartTime;
            newEvent.EndTime = ourEvent.EndTime;
            newEvent.StartDate = ourEvent.StartDate;
            newEvent.Organizer = ourEvent.Organizer;
            newEvent.SeatCount = ourEvent.SeatCount;
            newEvent.Place = ourEvent.Place;
            newEvent.Website = ourEvent.Website;
            newEvent.Phone = ourEvent.Phone;
            newEvent.MainCategoryId = ourEvent.MainCategoryId;
            await _db.OurEvents.AddAsync(newEvent);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult>  Update(int? id)
        {
            ViewBag.MainCategory = _db.MainCategories.ToList();
            if (id == null) return NotFound();
            OurEvent ourEvent = await _db.OurEvents.FindAsync(id);
            if (ourEvent == null) return NotFound();
            return View(ourEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, OurEvent ourEvent)
        {
            ViewBag.MainCategory = _db.MainCategories.ToList();
            if (id == null) return NotFound();
            OurEvent dbEvent = await _db.OurEvents.FindAsync(id);
            if (dbEvent == null) return NotFound();
            
            #region Photo
            if (ourEvent.Photo!=null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }
                if (!ourEvent.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }
                if (ourEvent.Photo.MaxLength(1000))
                {
                    ModelState.AddModelError("Photo", "Shekilin olchusu max 1000kb ola biler");
                    return View();
                }
                string path = Path.Combine("assets", "img", "event");
                string filename = await ourEvent.Photo.SaveImg(_env.WebRootPath, path);
                dbEvent.Image = filename;
            }
            #endregion
            OurEvent newEvent = new OurEvent();
            dbEvent.Title = ourEvent.Title;
            dbEvent.Email = ourEvent.Email;
            dbEvent.ShortDesc = ourEvent.ShortDesc;
            dbEvent.LongDesc = ourEvent.LongDesc;
            dbEvent.StartTime = ourEvent.StartTime;
            dbEvent.EndTime = ourEvent.EndTime;
            dbEvent.StartDate = ourEvent.StartDate;
            dbEvent.Organizer = ourEvent.Organizer;
            dbEvent.SeatCount = ourEvent.SeatCount;
            dbEvent.Place = ourEvent.Place;
            dbEvent.Website = ourEvent.Website;
            dbEvent.Phone = ourEvent.Phone;
            dbEvent.MainCategoryId = ourEvent.MainCategoryId;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            OurEvent ourEvent = await _db.OurEvents.FindAsync(id);
            if (ourEvent == null) return NotFound();

            //_db.MainCategories.Remove(category);
            //category.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int? id)
        {
            if (id == null) return View();
            OurEvent ourEvent = _db.OurEvents.Include(c => c.MainCategory).FirstOrDefault(p => p.Id == id);
            return View(ourEvent);
        }
    }
}