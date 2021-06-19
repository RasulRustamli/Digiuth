using System.ComponentModel.DataAnnotations;

namespace Digiuth.Models
{
    public class CourseContent
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
