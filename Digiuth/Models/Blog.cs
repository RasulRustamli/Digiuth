using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digiuth.Models
{
    public class Blog:SearchBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(110)]
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public string AuthorName { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public int MainCategoryId { get; set; }
        public MainCategory MainCategory { get; set; }
    }
}