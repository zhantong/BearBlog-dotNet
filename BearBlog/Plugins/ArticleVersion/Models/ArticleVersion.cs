using System.Linq;
using BearBlog.Models;

namespace BearBlog.Plugins.ArticleVersion.Models
{
    public class ArticleVersion
    {
        public int Id { get; set; }
        public int CollectionId { get; set; }
        public int VersionNumber { get; set; }
        public string VersionNote { get; set; }
        public virtual Article.Models.Article Article { get; set; }

        public static ArticleVersion AddArticle(BloggingContext bloggingContext, Article.Models.Article oldArticle,
            Article.Models.Article newArticle)
        {
            if (oldArticle == null)
            {
                var maxCollectionId =
                    bloggingContext.ArticleVersions.Max(v => (int?) v.CollectionId) ?? 0;
                var articleVersion = new ArticleVersion
                {
                    CollectionId = maxCollectionId + 1,
                    VersionNumber = 0,
                    VersionNote = "Initial",
                    Article = newArticle
                };
                bloggingContext.ArticleVersions.Add(articleVersion);
                bloggingContext.SaveChanges();
                return articleVersion;
            }

            return null;
        }
    }
}