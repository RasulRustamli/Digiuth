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
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
        public BlogController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var blogVm = new BlogVM
            {
                Blogs = _db.Blogs.ToList(),
                Bio = _db.Bios.FirstOrDefault()
            };
            ViewBag.MainCategories = _db.MainCategories.ToList();
            return View(blogVm);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            ViewBag.MainCategories = _db.MainCategories.ToList();
            if (id == null) return NotFound();
            Blog blog = await _db.Blogs.FindAsync(id);
            if (blog == null) return NotFound();
            BlogVM blogvm = new BlogVM
            {
                Blogs=_db.Blogs,
                Blog=blog,
                Bio=_db.Bios.FirstOrDefault()
            };
            
            return View(blogvm);
        }
    }
}
