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
    public class CertificatedUserController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public CertificatedUserController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_db.Certificates.ToList());
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Certificate certificate = await _db.Certificates.FindAsync(id);
            if (certificate == null) return NotFound();
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int? id)
        {
            if (id == null) return View();
            Certificate certificate = _db.Certificates.FirstOrDefault(p => p.Id == id);
            return View(certificate);
        }
        public IActionResult GetCertificate(int? id)
        {
            if (id == null) return View();
            Certificate certificate = _db.Certificates.FirstOrDefault(p => p.Id == id);
            return View(certificate);
        }

    }
}