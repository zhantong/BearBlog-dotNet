using System;
using System.Linq;
using BearBlog.Models;

namespace BearBlog.Plugins.Article.Models
{
    public class Events
    {
        public static void Register()
        {
            BearBlog.Models.Events.Restore += OnRestore;
        }

        private static void OnRestore(object? sender, RestoreEventArgs e)
        {
            using var db = new BloggingContext();
            var articlesElement = e.Root.GetProperty("article");
            foreach (var article in articlesElement.EnumerateArray())
            {
                var author = article.GetProperty("author").GetString();
                var user = db.Users.FirstOrDefault(u => u.Username == author);
                if (user == null)
                {
                    user = new User
                    {
                        Username = author
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                }

                var createdArticle = new Article
                {
                    Title = article.GetProperty("title").GetString(),
                    Body = article.GetProperty("body").GetString(),
                    Timestamp = DateTimeOffset.FromUnixTimeSeconds(article.GetProperty("timestamp").GetInt64())
                        .UtcDateTime,
                    Author = user
                };
                db.Articles.Add(createdArticle);
            }

            db.SaveChanges();
        }
    }
}