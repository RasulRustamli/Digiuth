using Digiuth.Models;
using System.Collections.Generic;

namespace Digiuth.ViewModels
{
    public class HomeVM
    {
        public Bio Bio { get; set; }
        public IEnumerable<Testimonial> Testimonials { get; set; }
        public AboutUs AboutUs { get; set; }
        public WatchUs WatchUs { get; set; }
        public ICollection<Course> Courses { get; set; }
        public IEnumerable<MainCategory> MainCategories { get; set; }
        public IEnumerable<OurEvent> OurEvents { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
    }
}