using System.Linq;
using BearBlog.Models;

namespace BearBlog.Plugins.ArticleVersion.Models
{
    public class ArticleVersion
    {
        public int Id { get; set; }
        public int VersionNumber { get; set; }
        public string VersionNote { get; set; }
        public string Status { get; set; }

        public Article.Models.Article Article { get; set; }
    }
}