using Digiuth.DAL;
using Digiuth.Models;
using Digiuth.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;
        public ContactController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ContactPageVM model = new ContactPageVM();
            model.Bio = _db.Bios.FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(Contact model)
        {
            if (string.IsNullOrEmpty(model.FirstName) ||
                string.IsNullOrEmpty(model.Email) ||
                string.IsNullOrEmpty(model.Message))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid) return View();
            var contact = new Contact
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Message = model.Message
            };
            await _db.AddAsync(contact);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
