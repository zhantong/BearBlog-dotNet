using System;
using System.Linq;
using System.Text.Json;
using BearBlog.Plugins.Article.Models;
using Microsoft.AspNetCore.Http;

namespace BearBlog.Models
{
    public static class Events
    {
        public static event EventHandler<RestoreEventArgs> Restore;

        public static void OnRestore(RestoreEventArgs e)
        {
            Restore?.Invoke(null, e);
        }

        public static event EventHandler<NavbarInitEventArgs> NavbarInit;

        public static void OnNavbarInit(NavbarInitEventArgs e)
        {
            NavbarInit?.Invoke(null, e);
        }

        public static event EventHandler<PatchArticleEventArgs> PatchArticle;

        public static void OnPatchArticle(PatchArticleEventArgs e)
        {
            PatchArticle?.Invoke(null, e);
        }

        public static event EventHandler<CreateArticleEventArgs> CreateArticle;

        public static void OnCreateArticle(CreateArticleEventArgs e)
        {
            CreateArticle?.Invoke(null, e);
        }

        public static event EventHandler<ListArticlesEventArgs> ListArticles;

        public static void OnListArticles(ListArticlesEventArgs e)
        {
            ListArticles?.Invoke(null, e);
        }

        public static void Init()
        {
            Plugins.Article.Models.Events.Register();
            Plugins.Category.Models.Events.Register();
            Plugins.Tag.Models.Events.Register();
            Plugins.Comment.Models.Events.Register();
            Plugins.ArticleVersion.Models.Events.Register();
            Plugins.Attachment.Models.Events.Register();
        }
    }

    public class RestoreEventArgs : EventArgs
    {
        public JsonElement Root;
    }

    public class NavbarInitEventArgs : EventArgs
    {
        public Navbar Navbar;
    }

    public class PatchArticleEventArgs : EventArgs
    {
        public Article TargetArticle;
        public Article Patch;
    }

    public class CreateArticleEventArgs : EventArgs
    {
        public Article Article;
    }

    public class ListArticlesEventArgs : EventArgs
    {
        public HttpRequest HttpRequest;
        public IQueryable<Article> Query;
    }
}