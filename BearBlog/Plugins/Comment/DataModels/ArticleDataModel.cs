using BearBlog.Models;

namespace BearBlog.Plugins.Article.DataModels
{
    public partial class ArticleDataModel
    {
        [Visibility(Visibility.Full)] public int? CommentCollectionId => _article.CommentCollectionId;
        public int CommentCount => _article.CommentCollection.Comments.Count;
    }
}