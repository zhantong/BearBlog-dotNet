using System.Collections.Generic;
using BearBlog.Models;

namespace BearBlog.Plugins.Category.Models
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
    }
}