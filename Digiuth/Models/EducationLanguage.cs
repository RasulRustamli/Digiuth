using System;
using System.Collections.Generic;

namespace Digiuth.Models
{
    public class EducationLanguage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
