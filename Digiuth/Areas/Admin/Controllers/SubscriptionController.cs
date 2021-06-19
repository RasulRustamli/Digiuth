using Digiuth.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Controllers
{
    [Area("Admin")]
    public class SubscriptionController : Controller
    {
        private readonly AppDbContext _db;
        public SubscriptionController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Subscriptions.ToList());
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var subscription = _db.Subscriptions.FirstOrDefault(x => x.Id == id);
            if (subscription == null) return NotFound();
            _db.Subscriptions.Remove(subscription);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
