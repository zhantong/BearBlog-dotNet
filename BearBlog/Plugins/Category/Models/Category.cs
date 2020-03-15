using System.Collections.Generic;

namespace BearBlog.Plugins.Category.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        public List<ArticleCategory> ArticleCategories { get; set; }
    }
}