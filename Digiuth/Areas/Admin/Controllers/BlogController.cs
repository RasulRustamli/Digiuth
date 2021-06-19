using Digiuth.DAL;
using Digiuth.Extentions;
using Digiuth.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;
        public BlogController(UserManager<AppUser> userManager,
                                SignInManager<AppUser> signInManager,
                                IWebHostEnvironment env,
                                RoleManager<IdentityRole> roleManager,
                                AppDbContext db)
        {
            _userManager = userManager;
            _env = env;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.MainCategory = _db.MainCategories.ToList();
            return View(_db.Blogs.Include(x=>x.MainCategory).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.MainCategory = _db.MainCategories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            ViewBag.MainCategory = _db.MainCategories.ToList();
            if (!ModelState.IsValid) return View();
            Blog newBlog = new Blog();
            newBlog.Title = blog.Title;
            newBlog.ShortDesc = blog.ShortDesc;
            newBlog.LongDesc = blog.LongDesc;
            newBlog.ShortDesc = blog.ShortDesc;
            newBlog.Date = blog.Date;
            newBlog.AuthorName = blog.AuthorName;
            newBlog.MainCategoryId = blog.MainCategoryId;
            #region Photo
            if (blog.Photo!=null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }
                if (!blog.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }
                if (blog.Photo.MaxLength(1000))
                {
                    ModelState.AddModelError("Photo", "Shekilin olchusu max 1000kb ola biler");
                    return View();
                }
                string path = Path.Combine("assets", "img", "blog");
                string filename = await blog.Photo.SaveImg(_env.WebRootPath, path);
                newBlog.Image = filename;
            }
            
            #endregion
            await _db.Blogs.AddAsync(newBlog);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.MainCategory = _db.MainCategories.ToList();
            if (id == null) return NotFound();
            Blog blog = await _db.Blogs.FindAsync(id);
            if (blog == null) return NotFound();
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Blog blog)
        {
            ViewBag.MainCategory = _db.MainCategories.ToList();
            if (id == null) return NotFound();
            Blog dbBlog = await _db.Blogs.FindAsync(id);
            if (blog == null) return NotFound();
            #region Photo
            if (blog.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }
                if (!blog.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }
                if (blog.Photo.MaxLength(1000))
                {
                    ModelState.AddModelError("Photo", "Shekilin olchusu max 1000kb ola biler");
                    return View();
                }
                string path = Path.Combine("assets", "img", "blog");
                string filename = await blog.Photo.SaveImg(_env.WebRootPath, path);
                dbBlog.Image = filename;
            }
            #endregion
            dbBlog.Title = blog.Title;
            dbBlog.ShortDesc = blog.ShortDesc;
            dbBlog.LongDesc = blog.LongDesc;
            dbBlog.ShortDesc = blog.ShortDesc;
            dbBlog.Date = blog.Date;
            dbBlog.MainCategoryId = blog.MainCategoryId;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Blog dbBlog = await _db.Blogs.FindAsync(id);
            if (dbBlog == null) return NotFound();
            _db.Blogs.Remove(dbBlog);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
