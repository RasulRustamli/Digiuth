using Digiuth.DAL;
using Digiuth.Extentions;
using Digiuth.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace azzuro.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;
        public TestimonialController(AppDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            var comments = _db.Testimonials.ToList();
            return View(comments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Testimonial testimonial)
        {
            if (!ModelState.IsValid) return View();
            #region Photo
            if (!testimonial.Photo.IsImage())
            {
                ModelState.AddModelError("Comment.Image", "Zehmet olmasa shekil formati sechin");
                return View();
            }

            if (testimonial.Photo.MaxLength(2000))
            {
                ModelState.AddModelError("Comment.Image", "Shekilin olchusu max 200kb ola biler");
                return View();
            }
            string path = Path.Combine("assets", "img", "testimonial");
            #endregion
            var newTestimonial = new Testimonial
            {
                FullName = testimonial.FullName,
                Position = testimonial.Position,
                Image = await testimonial.Photo.SaveImg(_env.WebRootPath, path),
                Description = testimonial.Description,
            };
            await _db.Testimonials.AddAsync(newTestimonial);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Testimonial testimonial = await _db.Testimonials.FindAsync(id);
            if (testimonial == null) return NotFound();
            return View(testimonial);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Testimonial testimonial)
        {
            if (id == null) return NotFound();
            Testimonial dbTestimonial = await _db.Testimonials.FindAsync(id);
            if (dbTestimonial == null) return NotFound();
            #region Photo
            if (testimonial.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }
                if (!testimonial.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }
                if (testimonial.Photo.MaxLength(1000))
                {
                    ModelState.AddModelError("Photo", "Shekilin olchusu max 1000kb ola biler");
                    return View();
                }
                string path = Path.Combine("assets", "img", "testimonial");
                string filename = await testimonial.Photo.SaveImg(_env.WebRootPath, path);
                dbTestimonial.Image = filename;
            }
            #endregion
            dbTestimonial.FullName = testimonial.FullName;
            dbTestimonial.Position = testimonial.Position;
            dbTestimonial.Description = dbTestimonial.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var testimonial = _db.Testimonials.FirstOrDefault(c => c.Id == id);
            if (testimonial == null) return NotFound();
            _db.Testimonials.Remove(testimonial);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
