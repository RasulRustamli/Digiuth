using Amazon.S3;
using Amazon.S3.Model;
using Digiuth.DAL;
using Digiuth.Extentions;
using Digiuth.Models;
using Digiuth.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Controllers
{
    public class VideoController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly IAmazonS3 AmazonS3;
        public VideoController(UserManager<AppUser> userManager,
                                SignInManager<AppUser> signInManager,
                                IWebHostEnvironment env,
                                IAmazonS3 amazonS3,
                                RoleManager<IdentityRole> roleManager,
                                AppDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
            _env = env;
            AmazonS3 = amazonS3;
        }
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Index(int id, string name)
        {

            try
            {
                //get current user
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                //check is course installed
                var userCourses = await _db.UserCourses
                    .FirstOrDefaultAsync(x => x.CourseId == id && x.AppUserId == user.Id);
                if (userCourses == null)
                {
                    return RedirectToAction("Index", "Course");
                }

                //get watchedlist
                var watchedList = _db.WatchedVideos
                    .Where(x => x.CourseId == id && x.UserId == user.Id.ToString())
                    .ToList();
                CourseVideo video = new CourseVideo();
                if (watchedList.Count() == 0)
                {
                    video = _db.CourseVideos
                   .FirstOrDefault(x => x.IsPreview == false && x.CourseId == id &&
                   x.Course.AppUser.UserName == name);
                }
                else
                {
                    UserWatchedVideo watchedVideo = _db.WatchedVideos.OrderByDescending(x => x.Id)
                        .FirstOrDefault(x => x.UserId == user.Id.ToString() && x.CourseId == id);
                    var videos = _db.CourseVideos
                        .Where(x => x.IsPreview == false && x.CourseId == id && x.Course.AppUser.UserName == name).ToList();
                    int index = videos.FindIndex(a => a.Id == watchedVideo.VideoId);

                    if (videos.Count > index + 1)
                    {
                        video = videos[index + 1];
                    }
                    else
                    {
                        video = videos[index];
                    }

                }
                var videoVM = new VideoVM
                {
                    CourseVideos = _db.CourseVideos
                    .Where(x => x.IsPreview == false && x.CourseId == id &&
                    x.Course.AppUser.UserName == name
                    ).ToList(),
                    CourseVideo = video,

                    MainCategories = _db.MainCategories.ToList(),
                    Course = _db.Courses.FirstOrDefault(x => x.Id == id)
                };
                return View(videoVM);
            }
            catch (Exception e)
            {
                
                  Console.WriteLine($"The directory was not found: '{e.Message}'");
            }
            return View();
        }
        public IActionResult Create(int id)
        {
            ViewBag.Id = id;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseVideo courseVideo,int CourseId)
        {
            if (!ModelState.IsValid) return View();
            
            if (ModelState["Video"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!courseVideo.Video.IsVideo())
            {
                ModelState.AddModelError("Video", "Zehmet olmasa video formati sechin");
                return View();
            }

            //aws setup
            var putRequest = new PutObjectRequest()
            {
                BucketName = "digiuth",
                Key = courseVideo.Video.FileName,
                InputStream = courseVideo.Video.OpenReadStream(),
                ContentType = courseVideo.Video.ContentType,
            };

            Course dbCourse =  _db.Courses.Include(x=>x.CourseVideos).FirstOrDefault(x => x.Id == courseVideo.CourseId);
            var result = await AmazonS3.PutObjectAsync(putRequest);
            string url = GeneratePreSignedURL(courseVideo.Video.FileName);

            ///get video duration
            DirectoryInfo dir = new DirectoryInfo(".");
            string dirName = dir.FullName;
            string duration;
            using (Process ffmpeg = new Process())
            {
                string resultReader;
                StreamReader errorreader;

                ffmpeg.StartInfo.UseShellExecute = false;
                ffmpeg.StartInfo.ErrorDialog = false;
                ffmpeg.StartInfo.RedirectStandardError = true;
                ffmpeg.StartInfo.FileName = Path.Combine(dirName, "ffmpeg", "ffmpeg.exe");
                ffmpeg.StartInfo.Arguments = "-i " + url;

                ffmpeg.Start();

                errorreader = ffmpeg.StandardError;
                ffmpeg.WaitForExit(100);

                resultReader = errorreader.ReadToEnd();
                duration = resultReader.Substring(resultReader.IndexOf("Duration: ") + ("Duration: ").Length, ("00:00:00.00").Length);

            }

            var newCourseVideo = new CourseVideo();
            if (dbCourse.CourseVideos.Count()==0)
            {
                newCourseVideo.IsPreview = true;
            }
            newCourseVideo.CourseId = CourseId;
            newCourseVideo.Title = courseVideo.Title;
            newCourseVideo.AwsVideoUrl = url;
            newCourseVideo.Duration = duration.Substring(0, duration.IndexOf("."));
            _db.CourseVideos.Add(newCourseVideo);
            await _db.SaveChangesAsync();
            return RedirectToAction("MyCourses","Course");
        }
        private string GeneratePreSignedURL(string objectKey)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = "digiuth",
                Key = objectKey,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddDays(7)

            };
            string url = AmazonS3.GetPreSignedURL(request);
            return url;
        }

        public async Task<IActionResult> GetVideoById(int id,int CourseId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var dbWatchedVideo = _db.WatchedVideos.FirstOrDefault(x=>x.VideoId==id&&x.UserId==user.Id.ToString());
            if (dbWatchedVideo==null)
            {
                UserWatchedVideo watchedVideo = new UserWatchedVideo
                {
                    UserId = user.Id.ToString(),
                    VideoId = id,
                    CourseId = CourseId
                };
                _db.WatchedVideos.Add(watchedVideo);
                _db.SaveChanges();
            }
            var videos = _db.CourseVideos
                .Where(x => x.IsPreview == false && x.CourseId==CourseId).ToList();
            
            int index = videos.FindIndex(a => a.Id == id);
            var video = videos[index+1];
            if (video==null)
            {
                video = videos[index];
            }
           
            return PartialView("_nextVideo", video);
        }


        public IActionResult Detail()
        {
            return View();
        }

    }
}
