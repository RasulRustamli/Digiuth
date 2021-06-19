using Digiuth.DAL;
using Digiuth.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BioController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public BioController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_db.Bios.FirstOrDefault());
        }

        public IActionResult Update()
        {
            Bio bio = _db.Bios.FirstOrDefault();
            if (bio == null) return NotFound();
            return View(bio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Bio bio)
        {
            Bio dbBio = _db.Bios.FirstOrDefault();
            if (bio == null) return NotFound();
            dbBio.Phone = bio.Phone;
            dbBio.Phone2 = bio.Phone2;
            dbBio.WordPress = bio.WordPress;
            dbBio.Pinterst = bio.Pinterst;
            dbBio.Email = bio.Email;
            dbBio.Email2 = bio.Email2;
            dbBio.Address = bio.Address;
            dbBio.Twitter = bio.Twitter;
            dbBio.Instagram = bio.Instagram;
            dbBio.Facebook = bio.Facebook;
            dbBio.AboutUs = bio.AboutUs;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}