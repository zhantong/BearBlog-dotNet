using System.Collections.Generic;
using System.Linq;
using BearBlog.Models;

namespace BearBlog.Plugins.Category.Models
{
    public class Events
    {
        public static void Register()
        {
            BearBlog.Models.Events.PatchArticle += OnPatchArticle;
            BearBlog.Models.Events.ListArticles += OnListArticles;
        }

        private static void OnPatchArticle(object? sender, PatchArticleEventArgs e)
        {
            using var db = new BloggingContext();
            if (e.Patch.ArticleCategories != null)
            {
                var articleCategories = new List<ArticleCategory>();
                foreach (var articleCategory in e.Patch.ArticleCategories)
                {
                    articleCategories.Add(new ArticleCategory
                        {ArticleId = e.TargetArticle.Id, CategoryId = articleCategory.CategoryId});
                }

                e.TargetArticle.ArticleCategories = articleCategories;
            }
        }

        private static void OnListArticles(object? sender, ListArticlesEventArgs e)
        {
            if (e.HttpRequest.Query.ContainsKey("category"))
            {
                using var db = new BloggingContext();
                var category = db.Categories.Single(c => c.Slug == e.HttpRequest.Query["category"].ToString());
                e.Query = e.Query.Where(a => a.ArticleCategories.Any(ac => ac.CategoryId == category.Id));
            }
        }
    }
}