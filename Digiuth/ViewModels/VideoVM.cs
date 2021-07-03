using Digiuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.ViewModels
{
    public class VideoVM
    {
        public IEnumerable<CourseVideo> CourseVideos { get; set; }
        public CourseVideo CourseVideo { get; set; }
        public IEnumerable<MainCategory> MainCategories { get; set; }
        public IEnumerable<CourseVideo> UserWatchedVideos { get; set; }
        public Course Course { get; set; }
        public Certificate Certificate { get; set; }
        public int VideoCount { get; set; }
    }
}
