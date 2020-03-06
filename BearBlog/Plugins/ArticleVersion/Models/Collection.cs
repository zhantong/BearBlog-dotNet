using System.Collections.Generic;

namespace BearBlog.Plugins.ArticleVersion.Models
{
    public class Collection
    {
        public int Id { get; set; }

        public List<Article.Models.Article> Articles;
    }
}