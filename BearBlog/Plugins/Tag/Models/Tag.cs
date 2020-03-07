using System.Collections.Generic;

namespace BearBlog.Plugins.Tag.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<ArticleTag> ArticleTags { get; set; }
    }
}