using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set;}
        [Required]
        public string  LastName { get; set;}
        public string  TeacherName { get; set;}
        public string  TeacherId { get; set;}
        public string CourseName { get; set; }
        public DateTime EndDate { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        public bool IsVerified { get; set; }
        public int CertificateCode { get; set; }
        public Certificate()
        {
            EndDate = DateTime.Now;
            var random = new Random();
            CertificateCode = random.Next(1000);
        }
    }
}
