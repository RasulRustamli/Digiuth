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
        public IEnumerable<MainCategory> MainCategories { get; set; }
        public Course Course { get; set; }
    }
}
