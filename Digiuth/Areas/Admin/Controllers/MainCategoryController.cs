using Digiuth.DAL;
using Digiuth.Extentions;
using Digiuth.Helpers;
using Digiuth.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MainCategoryController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _db;
        public MainCategoryController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.MainCategories.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MainCategory category)
        {
            if (!ModelState.IsValid) return View();
            bool existName = _db.MainCategories.Any(c => c.Name.ToLower().Trim() == category.Name.ToLower().Trim());
            if (existName)
            {
                ModelState.AddModelError("Name", "This name category is existed");
                return View();
            }
            #region Photo
            if (!ModelState.IsValid) return View();
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!category.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                return View();
            }
            if (category.Photo.MaxLength(1000))
            {
                ModelState.AddModelError("Photo", "Shekilin olchusu max 1000kb ola biler");
                return View();
            }
            string path = Path.Combine("assets","img", "category");
            string filename = await category.Photo.SaveImg(_env.WebRootPath, path);

            #endregion
            MainCategory newCategory = new MainCategory
            {
                Name = category.Name,
                WhyChooseCourse = category.WhyChooseCourse,
                VideoUrl = category.VideoUrl,
                LongDesc = category.LongDesc,
                ShortDesc = category.ShortDesc,
                PhotoUrl = filename
            };
            await _db.AddAsync(newCategory);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            MainCategory dbCategory = _db.MainCategories.FirstOrDefault(c => c.Id == id);
            if (dbCategory == null) return NotFound();
            return View(dbCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, MainCategory category)
        {

            if (id == null) return NotFound();
            MainCategory dbCategory = _db.MainCategories.FirstOrDefault(c => c.Id == id);
            if (dbCategory == null) return NotFound();
            MainCategory exisCategory = _db.MainCategories.FirstOrDefault(x => x.Name == category.Name);
            if (exisCategory != null)
            {
                if (dbCategory != exisCategory)
                {
                    ModelState.AddModelError("Name", "Bu adda Category movcuddur");
                    return View();
                }
            }
            #region Photo
            if (category.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }
                if (!category.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }
                if (category.Photo.MaxLength(2000))
                {
                    ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                    return View();
                }
                string path = Path.Combine("assets", "img", "category");
                Helper.DeleteImage(_env.WebRootPath, path, dbCategory.PhotoUrl);
                string fileName = await category.Photo.SaveImg(_env.WebRootPath, path);
                dbCategory.PhotoUrl = fileName;
            }
            #endregion
            dbCategory.Name = category.Name;
            dbCategory.WhyChooseCourse = category.WhyChooseCourse;
            dbCategory.VideoUrl = category.VideoUrl;
            dbCategory.LongDesc = category.LongDesc;
            dbCategory.ShortDesc = category.ShortDesc;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            MainCategory category = _db.MainCategories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            //_db.MainCategories.Remove(category);
            //category.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
