
namespace Digiuth.Models
{
    public class CourseSubject
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
