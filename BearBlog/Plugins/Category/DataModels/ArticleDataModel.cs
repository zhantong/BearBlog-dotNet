using System.Collections.Generic;
using System.Linq;
using BearBlog.Plugins.Category.DataModels;

namespace BearBlog.Plugins.Article.DataModels
{
    public partial class ArticleDataModel
    {
        public List<CategoryDataModel> Categories { get; set; }

        partial void Init(Models.Article article)
        {
            Categories = article.ArticleCategories.Select(ac => new CategoryDataModel(ac.Category)).ToList();
        }
    }
}