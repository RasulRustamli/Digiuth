using Amazon.S3;
using Amazon.S3.Model;
using Digiuth.DAL;
using Digiuth.Extentions;
using Digiuth.Models;
using Digiuth.ViewModels;
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
            this.AmazonS3 = amazonS3;
        }

        public IActionResult Index(int id,string name)
        {
            
            var videoVM = new VideoVM
            {
                CourseVideos = _db.CourseVideos
                .Where(x => x.IsPreview == false && x.CourseId == id &&
                x.Course.AppUser.UserName==name
                ).ToList(),
                MainCategories = _db.MainCategories.ToList(),
                Course = _db.Courses.FirstOrDefault(x => x.Id == id)
            };
            return View(videoVM);
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
    }
}
