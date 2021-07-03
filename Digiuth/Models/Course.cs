using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digiuth.Models
{
    public class Course: SearchBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public string WhatYouWillLearn { get; set; }
        public DateTime StartDate { get; set; }
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
        public bool IsVerified { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public bool IsFeatured{ get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ChildCategoryId { get; set; }
        public ChildCategory ChildCategory { get; set; }
        public int EducationLanguageId { get; set; }
        public EducationLanguage EducationLanguage { get; set; }
        public ICollection<CourseSubject> CourseSubjects { get; set; }
        public ICollection<CourseContent> CourseContents { get; set; }
        public ICollection<CourseVideo> CourseVideos { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }
    }
}