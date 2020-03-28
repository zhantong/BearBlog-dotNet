using System;
using BearBlog.Models;

namespace BearBlog.Plugins.Article.DataModels
{
    public partial class ArticleDataModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        [Visibility(Visibility.Full)] public string Body { get; set; }
        [Visibility(Visibility.Full)] public string BodyHtml { get; set; }
        public string BodyAbstract { get; set; }
        public DateTime Timestamp { get; set; }
        public User Author { get; set; }

        public ArticleDataModel(Models.Article article)
        {
            Id = article.Id;
            Title = article.Title;
            Slug = article.Slug;
            Body = article.Body;
            BodyHtml = article.BodyHtml;
            BodyAbstract = article.BodyAbstract;
            Timestamp = article.Timestamp;
            Author = article.Author;
            Init(article);
        }

        partial void Init(Models.Article article);
    }
}