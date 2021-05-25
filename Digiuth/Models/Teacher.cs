using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string Position { get; set; }
        public string Linkedin { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Behance { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public ICollection<Teacher>Teachers { get; set; }
    }
}
