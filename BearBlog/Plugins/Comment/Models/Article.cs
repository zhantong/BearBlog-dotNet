using BearBlog.Plugins.Comment.Models;

namespace BearBlog.Plugins.Article.Models
{
    public partial class Article
    {
        public int? CommentCollectionId { get; set; }
        public CommentCollection CommentCollection { get; set; }
    }
}