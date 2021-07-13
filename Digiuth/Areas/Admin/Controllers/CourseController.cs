using Digiuth.DAL;
using Digiuth.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public CourseController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Courses.Include(x => x.AppUser)
                                   .Include(x=>x.EducationLanguage)
                                   .Include(x=>x.ChildCategory).ToList());
        }

        public async Task<IActionResult> Active(int? id, bool IsVerified)
        {
            if (id == null) return NotFound();
            Course course = await _db.Courses.FindAsync(id);
            if (course == null) return NotFound();
            course.IsVerified = IsVerified;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Deactive(int? id, bool IsVerified)
        {
            if (id == null) return NotFound();
            Course course = await _db.Courses.FindAsync(id);
            if (course == null) return NotFound();
            course.IsVerified = !IsVerified;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
           // AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (id == null) return NotFound();
            Course course = await _db.Courses.FindAsync(id);
            if (course == null) return NotFound();
            var userWatchedVideo = _db.WatchedVideos
               .Where(x => x.CourseId == course.Id).ToList();
            var certificatedUsers = _db.Certificates.Where(x => x.CourseId == id).ToList();

            foreach (var item in userWatchedVideo)
            {
                _db.WatchedVideos.Remove(item);
            }
            foreach (var item in certificatedUsers)
            {
                _db.Certificates.Remove(item);
            }
            _db.Courses.Remove(course);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            Course course =  _db.Courses.Include(x=>x.AppUser).FirstOrDefault(x=>x.Id==id);
            if (course == null) return NotFound();
            return View(course);
        }

    public async Task<IActionResult> ActiveFeature(int? id, bool IsFeatured)
    {
        if (id == null) return NotFound();
        Course course = await _db.Courses.FindAsync(id);
        if (course == null) return NotFound();
        course.IsFeatured = IsFeatured;
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeactiveFeature(int? id, bool IsFeatured)
    {
        if (id == null) return NotFound();
        Course course = await _db.Courses.FindAsync(id);
        if (course == null) return NotFound();
        course.IsFeatured = !IsFeatured;
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
}
