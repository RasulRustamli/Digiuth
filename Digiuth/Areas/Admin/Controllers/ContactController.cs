using Digiuth.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;
        public ContactController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var contacts = _db.Contacts.Where(x => x.IsDeleted == false).ToList();
            return View(contacts);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var contact = _db.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null) return NotFound();
            contact.IsDeleted = true;
            //_db.Contacts.Remove(contact);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
