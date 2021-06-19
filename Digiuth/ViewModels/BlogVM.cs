using Digiuth.Models;
using System.Collections.Generic;
namespace Digiuth.ViewModels
{
    public class BlogVM
    {
        public Blog Blog { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public Bio Bio { get; set; }
    }
}