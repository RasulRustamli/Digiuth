using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digiuth.Models
{
    public class AboutUs
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string AboutContent { get; set; }
        public string AboutContent2 { get; set; }
        public int ExperienceYear { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}