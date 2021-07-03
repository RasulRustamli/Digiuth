
using Digiuth.DAL;
using Digiuth.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Controllers
{
    public class CertificateController : Controller
    {
        private readonly AppDbContext _db;
        public CertificateController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Certificate model)
        {
            if (string.IsNullOrEmpty(model.FirstName) ||
                string.IsNullOrEmpty(model.LastName))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid) return View();
            return RedirectToAction(nameof(Index));
        }
    }
}
