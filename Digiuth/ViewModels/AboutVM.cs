using Digiuth.Models;
using System.Collections.Generic;

namespace Digiuth.ViewModels
{
    public class AboutVM
    {
        public IEnumerable<Testimonial> Testimonials { get; set; }
        public AboutUs AboutUs { get; set; }
        public WatchUs WatchUs { get; set; }
        public IEnumerable<AppUser> User { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
    }
}