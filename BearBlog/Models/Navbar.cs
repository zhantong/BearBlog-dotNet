using System.Collections.Generic;

namespace BearBlog.Models
{
    public class Navbar
    {
        public string Brand { get; set; }
        public List<string> Items { get; set; } = new List<string>();
        public List<string> Templates { get; set; } = new List<string>();
    }
}