﻿using System;
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
                db.Articles.Add(new Article
                {
                    Title = article.GetProperty("title").GetString(),
                    Body = article.GetProperty("body").GetString(),
                    Timestamp = DateTimeOffset.FromUnixTimeSeconds(article.GetProperty("timestamp").GetInt64())
                        .UtcDateTime,
                    RepositoryId = Guid.Parse(article.GetProperty("version").GetProperty("repository_id").GetString()),
                    Status = article.GetProperty("version").GetProperty("status").GetString()
                });
            }

            db.SaveChanges();
        }
    }
}