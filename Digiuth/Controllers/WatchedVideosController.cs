using Digiuth.DAL;
using Digiuth.Models;
using Digiuth.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Controllers
{
    public class WatchedVideosController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _db;
        public WatchedVideosController(UserManager<AppUser> userManager,
                               AppDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        [Authorize]
        public async Task<IActionResult> Index(int id)
        {
            //get current user
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var userWatchedVideos = _db.WatchedVideos.Where(x => x.CourseId == id && x.UserId == user.Id.ToString()).ToList();
            var courseVideos = _db.CourseVideos
                    .Where(x => x.IsPreview == false && x.CourseId == id).ToList();
            var courseVideosLink = new List<CourseVideo>();
            foreach (var item in courseVideos)
            {
                foreach (var i in userWatchedVideos)
                {
                    if (item.Id == i.VideoId)
                    {
                        courseVideosLink.Add(item);
                    }
                }
            }
            
            var videoVM = new VideoVM
            {
                Course = _db.Courses.Include(x=>x.AppUser).FirstOrDefault(x => x.Id == id),
                UserWatchedVideos = courseVideosLink,
                VideoCount = courseVideos.Count(),
                MainCategories = _db.MainCategories.ToList(),
            };
            return View(videoVM);
        }


        [HttpPost]
        public async Task<IActionResult> Index(Certificate model)
        {
            //get current user
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (string.IsNullOrEmpty(model.FirstName) ||
                string.IsNullOrEmpty(model.LastName))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid) return View();
            
            var certificate = new Certificate
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                TeacherName = model.TeacherName,
                CourseName = model.CourseName,
                CourseId = model.CourseId,
                AppUserId = user.Id,
                IsVerified=false
            };
            var existUser = _db.Certificates
                .FirstOrDefault(x => x.AppUserId == user.Id && x.CourseId==model.CourseId);
            if (existUser == null)
            {
                await _db.Certificates.AddAsync(certificate);
                await _db.SaveChangesAsync();
            }
            
            return Json(existUser);
        }
    }
}
