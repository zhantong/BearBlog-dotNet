using BearBlog.Models;

namespace BearBlog.Plugins.Comment.Models
{
    public class Events
    {
        public static void Register()
        {
            BearBlog.Models.Events.CreateArticle += OnCreateArticle;
        }

        private static void OnCreateArticle(object? sender, CreateArticleEventArgs e)
        {
            using var db = new BloggingContext();
            var commentCollection = new CommentCollection();
            db.CommentCollections.Add(commentCollection);
            db.SaveChanges();
            e.Article.CommentCollection = commentCollection;
        }
    }
}