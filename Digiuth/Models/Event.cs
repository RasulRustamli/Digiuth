using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public DateTime Month { get; set; }
        public DateTime Day { get; set; }
        public string Organizer { get; set; }
        public string Place { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        public string ImageUrl { get; set; }
        
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
