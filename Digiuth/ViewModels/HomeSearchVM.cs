using Digiuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.ViewModels
{
    public class HomeSearchVM
    {
        public List<Blog> Blogs { get; set; }
        public List<MainCategory> MainCategories { get; set; }
        public List<OurEvent> Events { get; set; }
        public List<Course> Courses { get; set; }
        public List<AppUser> Teachers { get; set; }
    }
}
