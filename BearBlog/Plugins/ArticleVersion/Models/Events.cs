using BearBlog.Models;

namespace BearBlog.Plugins.ArticleVersion.Models
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
            var articleVersion = new ArticleVersion
            {
                VersionNumber = 1,
                VersionNote = "Initial",
                Status = "published"
            };
            db.ArticleVersions.Add(articleVersion);
            db.SaveChanges();
            e.Article.ArticleVersionId = articleVersion.Id;
        }
    }
}