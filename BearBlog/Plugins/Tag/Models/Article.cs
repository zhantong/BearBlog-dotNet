using System.Collections.Generic;
using BearBlog.Plugins.Tag.Models;

namespace BearBlog.Plugins.Article.Models
{
    public partial class Article
    {
        public ICollection<ArticleTag> ArticleTags { get; set; }
    }
}