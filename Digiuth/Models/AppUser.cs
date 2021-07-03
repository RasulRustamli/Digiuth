using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digiuth.Models
{
    public class AppUser: IdentityUser
    {
        [Required, MaxLength(20)]
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Position { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public bool IsTeacher { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public bool IsVerified { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public ICollection<Course> Course { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }
    }
}