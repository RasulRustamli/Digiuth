using Digiuth.DAL;
using Digiuth.Models;
using Digiuth.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public SearchController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Search(string search, string hidden)
        {
            List<AppUser> users = new List<AppUser>();
            IEnumerable<SearchBase> list = new List<SearchBase>();
            switch (hidden)
            {
                case "blog":
                    list = _db.Blogs
                        .Where(t => t.Title.ToLower()
                        .Contains(search.ToLower())).Take(5);
                    return PartialView("_BlogPartial", list);

                case "event":
                    list = _db.OurEvents
                        .Where(t => t.Title.ToLower()
                        .Contains(search.ToLower()))
                        .Take(5).ToList();
                    return PartialView("_EventPartial", list);

                case "course":
                    list = _db.Courses
                        .Where(t => t.Name.ToLower().Contains(search.ToLower())).Take(5).ToList();
                    return PartialView("_CoursePartial", list);

                case "maincategory":
                    list = _db.MainCategories
                        .Where(t => t.Name.ToLower().Contains(search.ToLower())).Take(5).ToList();
                    return PartialView("_MainCategoryPartial", list);
                default:
                    break;
            }
            return Ok(list);
        }

        public IActionResult HomeSearch(string search)
        {
            HomeSearchVM homeSearch = new HomeSearchVM
            {
                Events= _db.OurEvents
                       .Where(t => t.Title.ToLower()
                       .Contains(search.ToLower()))
                       .Take(3).ToList(),
                Blogs = _db.Blogs
                        .Where(t => t.Title.ToLower()
                        .Contains(search.ToLower())).Take(3).ToList(),
                Courses = _db.Courses
                       .Where(t => t.Name.ToLower().Contains(search.ToLower())).Take(3).ToList(),
                MainCategories= _db.MainCategories
                       .Where(t => t.Name.ToLower().Contains(search.ToLower())).Take(3).ToList(),
                Teachers=_db.Users.Where(x=>x.IsTeacher).Take(3).ToList(),
        };
            return PartialView("_HomeSearchPartial", homeSearch);

        }
    }
}
