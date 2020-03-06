using System.Collections.Generic;

namespace BearBlog.Plugins.Article.Models
{
    public partial class Article
    {
        public List<Category.Models.ArticleCategory> ArticleCategories { get; set; }
    }
}