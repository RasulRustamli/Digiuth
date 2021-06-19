using Digiuth.DAL;
using Digiuth.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationLanguageController : Controller
    {
        private readonly AppDbContext _db;
        public EducationLanguageController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.EducationLanguages.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EducationLanguage educationLanguage)
        {
            if (!ModelState.IsValid) return View();

            var newEducationLanguage = new EducationLanguage
            {
                Name = educationLanguage.Name
            };

            await _db.EducationLanguages.AddAsync(newEducationLanguage);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            var educationLanguage = _db.EducationLanguages.FirstOrDefault(c => c.Id == id);
            if (educationLanguage == null) return NotFound();
            return View(educationLanguage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, EducationLanguage educationLanguage)
        {

            if (id == null) return NotFound();
            var dbEducationLanguage = _db.EducationLanguages.FirstOrDefault(c => c.Id == id);
            if (dbEducationLanguage == null) return NotFound();

            dbEducationLanguage.Name = educationLanguage.Name;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var educationLanguage = _db.EducationLanguages.FirstOrDefault(c => c.Id == id);
            if (educationLanguage == null) return NotFound();

            //_db.MainCategories.Remove(category);
            //category.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}