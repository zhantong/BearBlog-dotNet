using System;
using System.Text.RegularExpressions;
using Markdig;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BearBlog.Plugins.Article.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string BodyHtml { get; set; }
        public string BodyAbstract { get; set; }

        public DateTime Timestamp { get; set; }

        public Guid RepositoryId { get; set; }
        public string Status { get; set; }
        public string VersionRemark { get; set; }

        public DateTime VersionTimestamp { get; set; }

        public Article()
        {
            Timestamp = DateTime.UtcNow;
            VersionTimestamp = DateTime.UtcNow;
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
            article.BodyAbstract = Regex.Replace(article.BodyHtml, "<[^<]+?>", "");
        }
    }
}