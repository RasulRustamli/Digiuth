using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public string WhatYouWillLearn { get; set; }
        public string VideoUrl { get; set; }
        public DateTime  StartDate { get; set; }
        public string Duration { get; set; }
        public string ClassDuration { get; set; }
        public string Institution { get; set; }
        public int SeatsAvailable { get; set; }
        public string Level { get; set; }
        public Decimal Price { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Behance { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public int TeacherId { get; set; }
        public int CategoryId { get; set; }
        public ICollection<CourseSubject> CourseSubjects { get; set; }
    }
}
