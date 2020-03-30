using System.Collections.Generic;
using System.Linq;
using BearBlog.Plugins.Tag.DataModels;

namespace BearBlog.Plugins.Article.DataModels
{
    public partial class ArticleDataModel
    {
        public ICollection<TagDataModel> Tags => _article.ArticleTags?.Select(ac => new TagDataModel(ac.Tag)).ToList();
    }
}