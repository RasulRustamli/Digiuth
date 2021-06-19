using Digiuth.DAL;
using Digiuth.Models;
using Digiuth.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Controllers
{
    public class MainCategoryController : Controller
    {
        private readonly AppDbContext _db;
        public MainCategoryController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            MainCategory category = await _db.MainCategories.FindAsync(id);
            if (category == null) return NotFound();
            MainCategoryVM mainCategory = new MainCategoryVM
            {
                Testimonials = _db.Testimonials,
                MainCategory = category,
                Courses = _db.Courses.Include(x=>x.AppUser).Where(x => x.ChildCategory.MainCategoryId==id && x.IsVerified)
            };
            return View(mainCategory);
        }
    }
}