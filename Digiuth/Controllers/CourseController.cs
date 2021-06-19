using Digiuth.DAL;
using Digiuth.Extentions;
using Digiuth.Models;
using Digiuth.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Controllers
{
    public class CourseController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public CourseController(UserManager<AppUser> userManager,
                                SignInManager<AppUser> signInManager,
                                IWebHostEnvironment env,
                                RoleManager<IdentityRole> roleManager,
                                AppDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
            _env = env;
        }

        public IActionResult Index()
        {
            CourseVM course = new CourseVM
            {
                Courses=_db.Courses,
                Testimonials=_db.Testimonials,
                MainCategories=_db.MainCategories
            };
            return View(course);
        }

        public IActionResult Create()
        {
            ViewBag.EdicationLanguage = _db.EducationLanguages.ToList();
            ViewBag.ChildCategory = _db.ChildCategories.ToList();
            ViewBag.MainCategory = _db.MainCategories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.EdicationLanguage = _db.EducationLanguages.ToList();
            ViewBag.ChildCategory = _db.ChildCategories.Include(x=>x.MainCategory).ToList();
            ViewBag.MainCategory = _db.MainCategories.ToList();
            #region Photo
            if (!ModelState.IsValid) return View();
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!course.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                return View();
            }
            if (course.Photo.MaxLength(1000))
            {
                ModelState.AddModelError("Photo", "Shekilin olchusu max 1000kb ola biler");
                return View();
            }
            string path = Path.Combine("assets", "img", "course");
            string filename = await course.Photo.SaveImg(_env.WebRootPath, path);
            #endregion
            Course newCourse = new Course();
            newCourse.Name = course.Name;
            newCourse.Image = filename;
            newCourse.ShortDesc = course.ShortDesc;
            newCourse.LongDesc = course.LongDesc;
            newCourse.WhatYouWillLearn = course.WhatYouWillLearn;
            newCourse.Website = course.Website;
            newCourse.StartDate = course.StartDate;
            newCourse.Duration = course.Duration;
            newCourse.ClassDuration = course.ClassDuration;
            newCourse.Institution = course.Institution;
            newCourse.SeatsAvailable = course.SeatsAvailable;
            newCourse.Level = course.Level;
            newCourse.Price = course.Price;
            newCourse.Facebook = course.Facebook;
            newCourse.Twitter = course.Twitter;
            newCourse.Behance = course.Behance;
            newCourse.Address = course.Address;
            newCourse.Phone = course.Phone;
            newCourse.IsVerified = false;


            newCourse.AppUserId = user.Id;
            newCourse.EducationLanguageId = course.EducationLanguageId;
            newCourse.ChildCategoryId = course.ChildCategoryId;
            
            await _db.Courses.AddAsync(newCourse);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Detail(int? id)
        {
            ViewBag.MainCategories = _db.MainCategories.ToList();
            if (id == null) return NotFound();
            Course course =  _db.Courses.Include(x=>x.AppUser)
                .Include(x=>x.ChildCategory)
                .ThenInclude(x=>x.MainCategory)
                .Include(x=>x.CourseVideos)
                .Include(x => x.CourseContents)
                .FirstOrDefault(x=>x.Id==id);
            if (course == null) return NotFound();
            return View(course);
        }

        public IActionResult GetChildCategoriesByMainCategory(int id)
        {
            var childCategories = _db.ChildCategories.Where(x=>x.MainCategoryId==id).ToList();
            return PartialView("_getChildCategoriesByMainPartial",childCategories);
        }

        public async Task<IActionResult> MyCourses()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(_db.Courses.Where(p => p.AppUserId==user.Id).ToList());
        }
        public async Task<IActionResult> UserInstalledCourse()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(_db.UserCourses
                .Where(p => p.AppUserId == user.Id)
                .Include(x=>x.Course)
                .ThenInclude(x=>x.AppUser).ToList());
        }

        public async Task<IActionResult> DeleteCourse(int? id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (id == null) return NotFound();
            Course course = await _db.Courses.FindAsync(id);
            if (course == null) return NotFound();
            if (course.AppUserId==user.Id)
            {
                _db.Courses.Remove(course);
            }
            else
            {
                return RedirectToAction("MyCourses", "Course");
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction("MyCourses", "Course");
        }

        public async Task<IActionResult> UpdateCourse(int? id)
        {
            ViewBag.EdicationLanguage = _db.EducationLanguages.ToList();
            ViewBag.ChildCategory = _db.ChildCategories.ToList();
            ViewBag.MainCategory = _db.MainCategories.ToList();
            if (id == null) return NotFound();
            Course course = await _db.Courses.FindAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCourse(Course course, int? id)
        {
            if (id == null) return NotFound();
            Course dbCourse = await _db.Courses.FindAsync(id);
            if (course == null) return NotFound();
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.EdicationLanguage = _db.EducationLanguages.ToList();
            ViewBag.ChildCategory = _db.ChildCategories.Include(x => x.MainCategory).ToList();
            ViewBag.MainCategory = _db.MainCategories.ToList();
            if (dbCourse.AppUserId==user.Id)
            {
                #region Photo
                if (course.Photo != null)
                {
                    if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    {
                        return View();
                    }
                    if (!course.Photo.IsImage())
                    {
                        ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                        return View();
                    }
                    if (course.Photo.MaxLength(1000))
                    {
                        ModelState.AddModelError("Photo", "Shekilin olchusu max 1000kb ola biler");
                        return View();
                    }
                    string path = Path.Combine("assets", "img", "course");
                    string filename = await course.Photo.SaveImg(_env.WebRootPath, path);
                    dbCourse.Image = filename;
                }
                #endregion
                dbCourse.Name = course.Name;
                dbCourse.ShortDesc = course.ShortDesc;
                dbCourse.LongDesc = course.LongDesc;
                dbCourse.WhatYouWillLearn = course.WhatYouWillLearn;
                dbCourse.Website = course.Website;
                dbCourse.StartDate = course.StartDate;
                dbCourse.Duration = course.Duration;
                dbCourse.ClassDuration = course.ClassDuration;
                dbCourse.Institution = course.Institution;
                dbCourse.SeatsAvailable = course.SeatsAvailable;
                dbCourse.Level = course.Level;
                dbCourse.Price = course.Price;
                dbCourse.Facebook = course.Facebook;
                dbCourse.Twitter = course.Twitter;
                dbCourse.Behance = course.Behance;
                dbCourse.Address = course.Address;
                dbCourse.Phone = course.Phone;

                dbCourse.AppUserId = user.Id;
                dbCourse.EducationLanguageId = course.EducationLanguageId;
                dbCourse.ChildCategoryId = course.ChildCategoryId;
                await _db.SaveChangesAsync();
            }
            else
            {
                return RedirectToAction("MyCourses", "Course");
            }
            return RedirectToAction("MyCourses", "Course");
        }

        public IActionResult AddContent(int? id)
        {
            ViewBag.CourseId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContent(CourseContent courseContent, int CourseId)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Courses = _db.Courses.Where(x => x.AppUserId == user.Id).ToList();
            CourseContent newCourseContent = new CourseContent();
            newCourseContent.Content = courseContent.Content;
            newCourseContent.CourseId = CourseId;
            await _db.CourseContents.AddAsync(newCourseContent);
            await _db.SaveChangesAsync();
            return RedirectToAction("Detail", "Course", new { @id = CourseId });
        }

        public async Task<IActionResult> DeleteContent(int? id)
        {
            if (id == null) return NotFound();
            CourseContent content = _db.CourseContents.Include(x=>x.Course).ThenInclude(x=>x.AppUser).FirstOrDefault(x=>x.Id==id);
            if (content == null) return NotFound();
            if (content.Course.AppUser.UserName == User.Identity.Name)
            {
                _db.CourseContents.Remove(content);
            }
            else
            {
                return RedirectToAction("Detail", "Course", new { @id = content.CourseId });
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Detail", "Course", new { @id = content.CourseId });
        }
    }
}