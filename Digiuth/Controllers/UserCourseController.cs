using Digiuth.DAL;
using Digiuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Controllers
{
    public class UserCourseController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public UserCourseController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        [Authorize(Roles ="Student")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Create(int id)
        {
           
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
           
            UserCourse userCourse = new UserCourse
            {
                CourseId = id,
                AppUserId = user.Id
            };
            await _db.UserCourses.AddAsync(userCourse);
            await _db.SaveChangesAsync();
            return RedirectToAction("Detail", "Course", new { @id = id });
        }
    }
}
