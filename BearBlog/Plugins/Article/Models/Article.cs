using System;
using System.Text.RegularExpressions;
using BearBlog.Models;
using Markdig;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BearBlog.Plugins.Article.Models
{
    public partial class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        [Visibility(Visibility.Full)] public string Body { get; set; }
        [Visibility(Visibility.Full)] public string BodyHtml { get; set; }
        public string BodyAbstract { get; set; }

        public DateTime Timestamp { get; set; }

        public int? AuthorId { get; set; }

        public User Author { get; set; }

        public Article()
        {
            Timestamp = DateTime.UtcNow;
        }

        public static void OnEntityTracked(object sender, EntityTrackedEventArgs e)
        {
            if (e.Entry.Entity is Article article)
            {
                ParseMarkdown(article);
            }
        }

        public static void OnEntityStateChanged(object sender, EntityStateChangedEventArgs e)
        {
            if (e.Entry.Entity is Article article)
            {
                ParseMarkdown(article);
            }
        }

        private static void ParseMarkdown(Article article)
        {
            article.BodyHtml = Markdown.ToHtml(article.Body);
            var escapedBodyHtml = Regex.Replace(article.BodyHtml, "<[^<]+?>", "");
            article.BodyAbstract = escapedBodyHtml.Substring(0, Math.Min(escapedBodyHtml.Length, 200)) + "...";
        }
    }
}