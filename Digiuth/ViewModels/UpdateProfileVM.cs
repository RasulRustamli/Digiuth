using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digiuth.ViewModels
{
    public class UpdateProfileVM
    {
        [StringLength(50)]
        public string FullName { get; set; }
        
        [StringLength(50)]
        public string WebSite { get; set; }
        
        [StringLength(50)]
        public string Position { get; set; }
        
        [StringLength(50)]
        public string Phone { get; set; }
        
        [StringLength(50)]
        public string Facebook { get; set; }

        [StringLength(50)]
        public string Twitter { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
        
        [StringLength(200)]
        public string Address { get; set; }

        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}