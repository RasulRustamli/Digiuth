using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Models
{
    public class UserCourse
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
