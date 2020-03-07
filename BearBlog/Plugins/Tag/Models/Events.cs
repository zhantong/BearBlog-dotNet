using System.Linq;
using BearBlog.Models;

namespace BearBlog.Plugins.Tag.Models
{
    public class Events
    {
        public static void Register()
        {
            BearBlog.Models.Events.PatchArticle += OnPatchArticle;
        }

        private static void OnPatchArticle(object? sender, PatchArticleEventArgs e)
        {
            using var db = new BloggingContext();
            if (e.Patch.ArticleTags != null)
            {
                var articleTags = e.Patch.ArticleTags.Select(articleTag =>
                    new ArticleTag {ArticleId = e.TargetArticle.Id, TagId = articleTag.TagId}).ToList();
                e.TargetArticle.ArticleTags = articleTags;
            }
        }
    }
}