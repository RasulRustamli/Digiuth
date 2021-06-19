using Digiuth.DAL;
using Digiuth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChildCategoryController : Controller
    {
        private readonly AppDbContext _db;
        public ChildCategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.ChildCategories.Include(x=>x.MainCategory).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.MainCategory = _db.MainCategories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChildCategory category)
        {
            ViewBag.MainCategory = _db.MainCategories.ToList();
            if (!ModelState.IsValid) return View();
            bool existName = _db.ChildCategories.Any(c => c.Name.ToLower().Trim() == category.Name.ToLower().Trim());
            if (existName)
            {
                ModelState.AddModelError("Name", "This name category is existed");
                return View();
            }
            ChildCategory newCategory = new ChildCategory
            {
                Name = category.Name,
                MainCategoryId=category.MainCategoryId
            };

            await _db.AddAsync(newCategory);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            ViewBag.MainCategory = _db.MainCategories.ToList();
            if (id == null) return NotFound();
            ChildCategory dbCategory = _db.ChildCategories.FirstOrDefault(c => c.Id == id);
            if (dbCategory == null) return NotFound();
            return View(dbCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, ChildCategory category)
        {
            ViewBag.MainCategory = _db.MainCategories.ToList();
            if (id == null) return NotFound();
            ChildCategory dbCategory = _db.ChildCategories.FirstOrDefault(c => c.Id == id);
            if (dbCategory == null) return NotFound();
            ChildCategory exisCategory = _db.ChildCategories.FirstOrDefault(x => x.Name == category.Name);
            if (exisCategory != null)
            {
                if (dbCategory != exisCategory)
                {
                    ModelState.AddModelError("Name", "Bu adda Category movcuddur");
                    return View();
                }
            }
            dbCategory.MainCategoryId = category.MainCategoryId;
            dbCategory.Name = category.Name;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var category = _db.ChildCategories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            //category.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
