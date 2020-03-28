using System;
using BearBlog.DataModels;
using BearBlog.Models;

namespace BearBlog.Plugins.Article.DataModels
{
    public partial class ArticleDataModel
    {
        private readonly Models.Article _article;
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        [Visibility(Visibility.Full)] public string Body { get; set; }
        [Visibility(Visibility.Full)] public string BodyHtml { get; set; }
        public string BodyAbstract { get; set; }
        public DateTime Timestamp { get; set; }
        public UserDataModel Author { get; set; }

        public ArticleDataModel(Models.Article article)
        {
            _article = article;
            Id = article.Id;
            Title = article.Title;
            Slug = article.Slug;
            Body = article.Body;
            BodyHtml = article.BodyHtml;
            BodyAbstract = article.BodyAbstract;
            Timestamp = article.Timestamp;
            Author = new UserDataModel(article.Author);
        }
    }
}