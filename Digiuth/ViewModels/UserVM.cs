using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.ViewModels
{
    public class UserVM
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Position { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Behance { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public bool IsVerified { get; set; }
        //public List<string> Roles { get; set; }
    }
}
