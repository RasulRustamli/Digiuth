using Digiuth.Models;
using System.Collections.Generic;

namespace Digiuth.ViewModels
{
    public class MainCategoryVM
    {
        public MainCategory MainCategory { get; set; }
        public IEnumerable<Testimonial> Testimonials { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}