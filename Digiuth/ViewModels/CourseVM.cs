using Digiuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.ViewModels
{
    public class CourseVM
    {
        public IEnumerable<Testimonial> Testimonials { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<MainCategory> MainCategories { get; set; }
    }
}
