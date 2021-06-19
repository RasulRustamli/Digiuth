using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digiuth.Models
{
    public class MainCategory: SearchBase
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string LongDesc { get; set; }
        public string ShortDesc { get; set; }
        public string PhotoUrl { get; set; }
        public string WhyChooseCourse { get; set; }
        public string VideoUrl { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public ICollection<ChildCategory> ChildCategories { get; set; }
        public ICollection<OurEvent> Events { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}