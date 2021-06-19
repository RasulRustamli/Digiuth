using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digiuth.Models
{
    public class CourseVideo
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string AwsVideoUrl { get; set; }
        public bool IsPreview { get; set; }
        public string Duration { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Video { get; set; }
    }
}